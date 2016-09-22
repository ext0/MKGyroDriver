using MKGyroDriver.YoctoNative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MKGyroDriver
{
    public static class Gyro
    {
        private static readonly String GYRO_SUFFIX = ".gyro";
        private static YGyro _gyro = null;

        public static bool IsConnected
        {
            get
            {
                if (_gyro == null)
                {
                    return false;
                }
                return _gyro.isSensorReady();
            }
        }

        public static bool RegisterHub()
        {
            String errorMessage = null;
            if (YAPI.RegisterHub("usb", ref errorMessage) != YAPI.SUCCESS)
            {
                throw new Exception("Could not register hub: " + errorMessage);
            }
            return true;
        }

        public static bool UpdateEvents()
        {
            String errorMessage = null;
            if (YAPI.HandleEvents(ref errorMessage) != YAPI.SUCCESS)
            {
                throw new Exception("Failed to handle events! " + errorMessage);
            }
            return true;
        }

        public static bool RegisterGyroscope()
        {
            _gyro = YGyro.FirstGyro();
            if (_gyro == null)
            {
                throw new Exception("No USB gyroscope found!");
            }
            _gyro = YGyro.FindGyro(_gyro.get_module().get_serialNumber() + GYRO_SUFFIX);
            if (_gyro == null)
            {
                throw new Exception("Yocto device found, but no gyroscope could be conneceted to!");
            }
            return true;
        }

        public static void RegisterQuaternionCallback(YGyro.QuatCallback quaternionCallback)
        {
            if (_gyro != null)
            {
                _gyro.registerQuaternionCallback(quaternionCallback);
            }
            else
            {
                throw new InvalidOperationException("Cannot register callback without first registering hub and gyroscope.");
            }
        }
    }
}
