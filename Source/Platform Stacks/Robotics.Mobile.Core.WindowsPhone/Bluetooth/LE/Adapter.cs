using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace Robotics.Mobile.Core.Bluetooth.LE
{
    public class Adapter : IAdapter
    {
        //events
        public event EventHandler<DeviceDiscoveredEventArgs> DeviceDiscovered;
        public event EventHandler<DeviceConnectionEventArgs> DeviceConnected;
        public event EventHandler<DeviceConnectionEventArgs> DeviceDisconnected;
        public event EventHandler ScanTimeoutElapsed;

        private CancellationToken _DeviceScannerCancellationToken;
        private Task<DeviceInformationCollection> _DeviceScanner;

        //class members
        
        public bool IsScanning
        {
            get { return this._isScanning;  }
        } protected bool _isScanning;

        public IList<IDevice> DiscoveredDevices
        {
            get { return this._discoveredDevices; }
        } protected IList<IDevice> _discoveredDevices = new List<IDevice>(); 

        public IList<IDevice> ConnectedDevices
        {
            get { return this._connectedDevices; }
        } protected IList<IDevice> _connectedDevices = new List<IDevice>();

        public void StartScanningForDevices()
        {
            StartScanningForDevices(serviceUuid: Guid.Empty);
        }

        public void StartScanningForDevices(Guid serviceUuid)
        {
            if (this._isScanning)
                return;

            this._discoveredDevices = new List<IDevice>();

            this._isScanning = true;

            _DeviceScanner = DeviceInformation.FindAllAsync(BluetoothDevice.GetDeviceSelector())
                .AsTask(this._DeviceScannerCancellationToken)
                .ContinueWith<DeviceInformationCollection>(async (t) =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                        return null;

                    foreach (var item in t.Result)
                    {
                        var btDevice = await BluetoothDevice.FromIdAsync(item.Id);
                        var d = new Device(btDevice);
                        this._discoveredDevices.Add(d);
                        this.DeviceDiscovered(this, new DeviceDiscoveredEventArgs() { Device = d });
                    }

                    this._isScanning = false;

                    return t.Result;
                });

        }

        protected bool DeviceExistsInDiscoveredList(BluetoothLEDevice device)
        {
            foreach (var d in _discoveredDevices)
            {
                if (device.BluetoothAddress == ((BluetoothLEDevice) d.NativeDevice).BluetoothAddress)
                    return true;
            }
            return false;
        }

        public void StopScanningForDevices()
        {
            throw new NotImplementedException();
        }

        public void ConnectToDevice(IDevice device)
        {
            //TODO ConectToDevice
 
            this._connectedDevices.Add(device);
            DeviceConnected(this, new DeviceConnectionEventArgs() {Device = device, ErrorMessage = "error"});
        }

        public void DisconnectDevice(IDevice device)
        {
            //TODO DisconnetDevice
            this._connectedDevices.Remove(device);
        }

    }
}
