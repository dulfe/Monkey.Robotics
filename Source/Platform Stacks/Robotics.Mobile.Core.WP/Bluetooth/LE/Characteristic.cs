using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Storage.Streams;

namespace Robotics.Mobile.Core.Bluetooth.LE
{
    public class Characteristic : ICharacteristic
    {
        public event EventHandler<CharacteristicReadEventArgs> ValueUpdated;

        protected GattCharacteristicWithValue _gattCharacteristicWithValue;
        private bool _notifiedEnabled = false;

        public Characteristic(GattCharacteristicWithValue gattCharacteristicWithValue)
        {
            this._gattCharacteristicWithValue = gattCharacteristicWithValue;
        }

        public Characteristic(GattCharacteristic nativeCharacteristic)
        {
            this._gattCharacteristicWithValue = new GattCharacteristicWithValue(nativeCharacteristic);
        }

        public Guid ID
        {
            get { return _gattCharacteristicWithValue.ID; }
        }

        public string Uuid
        {
            get { return _gattCharacteristicWithValue.Uuid.ToString(); }
        }

        private byte[] _Value;

        public byte[] Value
        {
            get
            {
                if (!_notifiedEnabled)
                    _Value = ReadValue().Result;

                return _Value;
            }
        }

        public string StringValue
        {
            get
            {
                var value = Value;

                return Encoding.UTF8.GetString(value, 0, value.Length);
            }
        }

        public IList<IDescriptor> Descriptors
        {
            get
            {
                if (_descriptors == null)
                {
                    _descriptors = new List<IDescriptor>();

                    foreach (KnownDescriptor kd in KnownDescriptors.GetDescriptors())
                    {
                        //var d = _gattCharacteristicWithValue.NativeCharacteristic.GetDescriptors(kd.ID)[0];
                        var descriptors = _gattCharacteristicWithValue.NativeCharacteristic.GetDescriptors(kd.ID);
                        if (descriptors.Count > 0)
                        {
                            _descriptors.Add(new Descriptor(descriptors[0]));
                        }
                    }
                }
                return _descriptors;
            }
        }
        private IList<IDescriptor> _descriptors = null;

        public object NativeCharacteristic
        {
            get { return _gattCharacteristicWithValue; }
        }

        public string Name
        {
            get { return KnownCharacteristics.Lookup(this.ID).Name; }
        }

        public CharacteristicPropertyType Properties
        {
            get { return (CharacteristicPropertyType)(int)this._gattCharacteristicWithValue.NativeCharacteristic.CharacteristicProperties; }
        }

        public bool CanRead
        {
            get
            {
                if (CheckGattProperty(GattCharacteristicProperties.Read))
                    return true;
                return false;
            }
        }

        public bool CanUpdate
        {
            get
            {
                if (CheckGattProperty(GattCharacteristicProperties.Notify))
                    return true;
                return false;
            }
        }

        public bool CanWrite
        {
            get
            {
                if (CheckGattProperty(GattCharacteristicProperties.Write) || CheckGattProperty(GattCharacteristicProperties.WriteWithoutResponse))
                    return true;
                return false;
            }
        }

        public async Task<byte[]> ReadValue()
        {
            var result = await _gattCharacteristicWithValue.NativeCharacteristic.ReadValueAsync(BluetoothCacheMode.Uncached);

            if (result.Status == GattCommunicationStatus.Success)
            {
                byte[] forceData = new byte[result.Value.Length];
                DataReader.FromBuffer(result.Value).ReadBytes(forceData);
                return forceData;
            }
            else
            {
                return null;
            }
        }

        public async void StartUpdates()
        {
            //if (CanRead)
            //{
            //    Debug.WriteLine("** Characteristic.RequestValue, PropertyType = Read, requesting read");
            //    _gattCharacteristicWithValue.NativeCharacteristic.ValueChanged += UpdatedRead;
            //}
            if (CanUpdate)
            {
                if (!_notifiedEnabled)
                {
                    Debug.WriteLine("** Characteristic.RequestValue, PropertyType = Notify, requesting updates");

                    _gattCharacteristicWithValue.NativeCharacteristic.ValueChanged += UpdatedNotify;
                    await RegisterForUpdates();
                    _notifiedEnabled = true;
                }
                else
                {
                    Debug.WriteLine("StartUpdates: Already receiving updates.");
                }
            }
        }

        async Task RegisterForUpdates()
        {
            await this._gattCharacteristicWithValue.NativeCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);

            if (this.Descriptors.Count > 0)
            {
                //TODO

            }
            else
            {
                Debug.WriteLine("RequestValue, FAILED: _nativeCharacteristic.Descriptors was empty, not sure why");
            }

        }

        public async void StopUpdates()
        {
            await this._gattCharacteristicWithValue.NativeCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.None);

            _gattCharacteristicWithValue.NativeCharacteristic.ValueChanged -= UpdatedNotify;
            _notifiedEnabled = false;
        }


        void UpdatedNotify(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            _Value = new byte[args.CharacteristicValue.Length];
            Windows.Storage.Streams.DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(_Value);

            this.ValueUpdated(this, new CharacteristicReadEventArgs()
            {
                Characteristic = this
            });

            Debug.WriteLine("Characteristic Value Changed");
        }

        public async Task<ICharacteristic> ReadAsync()
        {
            var val = new GattCharacteristicWithValue();
            val.NativeCharacteristic = this._gattCharacteristicWithValue.NativeCharacteristic;

            if (!CanRead)
            {
                throw new InvalidOperationException("Characteristic does not support READ");
            }

            try
            {
                GattReadResult readResult = await this._gattCharacteristicWithValue.NativeCharacteristic.ReadValueAsync();

                if (readResult.Status == GattCommunicationStatus.Success)
                {
                    val.Value = new byte[readResult.Value.Length];
                    DataReader.FromBuffer(readResult.Value).ReadBytes(val.Value);
                }
            }
            catch { }

            //TODO: I don't understand this method ..... 
            return new Characteristic(val);


        }

        public async void Write(byte[] data)
        {
            Debug.WriteLine("Write received:" + data.ToString());

            var dataWriter = new DataWriter();

            dataWriter.WriteBytes(data);

            var buffer = dataWriter.DetachBuffer();

            await _gattCharacteristicWithValue.NativeCharacteristic.WriteValueAsync(buffer, GattWriteOption.WriteWithoutResponse);
        }

        public bool CheckGattProperty(GattCharacteristicProperties gattProperty)
        {
            if (((int)_gattCharacteristicWithValue.NativeCharacteristic.CharacteristicProperties & (int)gattProperty) != 0)
            {
                return true;
            }
            return false;
        }

    }

    //GattCharacteristic is sealed so we can't inherit 
    public class GattCharacteristicWithValue
    {
        public GattCharacteristicWithValue() { }

        public GattCharacteristicWithValue(GattCharacteristic gattCharacteristic)
        {
            this.NativeCharacteristic = gattCharacteristic;
        }

        public GattCharacteristic NativeCharacteristic { get; set; }

        public byte[] Value { get; set; }

        public Guid ID { get { return NativeCharacteristic.Uuid; } }

        public string Uuid
        {
            get { return NativeCharacteristic.Uuid.ToString(); }
        }


    }
}
