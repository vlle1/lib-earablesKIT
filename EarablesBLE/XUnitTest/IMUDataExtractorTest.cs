using EarablesKIT.Models.Library;
using System;
using Xunit;
using static EarablesKIT.Models.Library.IMUDataExtractor;


namespace XUnitTest
{
    /// <summary>
    /// This class test the static methods from the class IMUDataExtractor
    /// </summary>
    public class IMUDataExtractorTest
    {
        /// <summary>
        /// Tests the method ExtractIMUDataString
        /// </summary>
        [Fact]
        public void ExtractStringTest()
        {
            byte[] byteIMUData = new byte[16] { 0x55, 1, 42, 0x0C, 0b_00000000, 0b_11000101, 0b_00000011,
            0b_01101100, 0b_00000011, 0b_11100111, 0b_00100000, 0b_10100001, 0b_10011110, 0b00011100, 0b_00000100, 0b_00000110 };
            double accScaleFactor = 8192;
            double gyroScaleFactor = 65.5;
            byte[] offset = new byte[15];
            IMUDataEntry entry = ExtractIMUDataString(byteIMUData, accScaleFactor, gyroScaleFactor, offset);

            // Tests the right calculation for the Acceleration in G
            Assert.Equal((10 / 9.80665), (double)entry.Acc.G_X, 2);
            Assert.Equal(-30 / 9.80665, (double)entry.Acc.G_Y, 2);
            Assert.Equal(1.234 / 9.80665, (double)entry.Acc.G_Z, 2);

            // Tests the right calculation for the Acceleration in M/s
            Assert.Equal(10, (double)entry.Acc.MperS_X, 2);
            Assert.Equal(-30, (double)entry.Acc.MperS_Y, 2);
            Assert.Equal(1.234, (double)entry.Acc.MperS_Z, 2);

            // Tests the right calculation for the Gyroscope in Deg/s
            Assert.Equal(3.007, (double)entry.Gyro.DegsPerSec_X, 2);
            Assert.Equal(13.37, (double)entry.Gyro.DegsPerSec_Y, 2);
            Assert.Equal(15.25, (double)entry.Gyro.DegsPerSec_Z, 2);
        }


        /// <summary>
        /// Test the method ExtractIMURangeAccelerometer
        /// </summary>
        [Fact]
        public void ExtractIMURangeAccelerometerTest1()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x29;
            int range = ExtractIMURangeAccelerometer(bytes);

            Assert.Equal(4, range);
        }

        /// <summary>
        /// Test the method ExtractIMURangeAccelerometer
        /// </summary>
        [Fact]
        public void ExtractIMURangeAccelerometerTest2()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x24;
            int range = ExtractIMURangeAccelerometer(bytes);

            Assert.Equal(2, range);
        }

        /// <summary>
        /// Test the method ExtractIMURangeAccelerometer
        /// </summary>
        [Fact]
        public void ExtractIMURangeAccelerometerTest3()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x34;
            int range = ExtractIMURangeAccelerometer(bytes);

            Assert.Equal(8, range);
        }

        /// <summary>
        /// Test the method ExtractIMURangeAccelerometer
        /// </summary>
        [Fact]
        public void ExtractIMURangeAccelerometerTest4()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x3C;
            int range = ExtractIMURangeAccelerometer(bytes);

            Assert.Equal(16, range);
        }

        /// <summary>
        /// Tests the method ExtractIMUScaleFactorAccelerometer
        /// </summary>
        [Fact]
        public void ExtractIMUScaleFactorAccelerometerTest1()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x29;
            int range = ExtractIMUScaleFactorAccelerometer(bytes);

            Assert.Equal(8192, range);
        }

        /// <summary>
        /// Tests the method ExtractIMUScaleFactorAccelerometer
        /// </summary>
        [Fact]
        public void ExtractIMUScaleFactorAccelerometerTest2()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x24;
            int range = ExtractIMUScaleFactorAccelerometer(bytes);

            Assert.Equal(16384, range);
        }

        /// <summary>
        /// Tests the method ExtractIMUScaleFactorAccelerometer
        /// </summary>
        [Fact]
        public void ExtractIMUScaleFactorAccelerometerTest3()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x34;
            int range = ExtractIMUScaleFactorAccelerometer(bytes);

            Assert.Equal(4096, range);
        }


        /// <summary>
        /// Tests the method ExtractIMUScaleFactorAccelerometer
        /// </summary>
        [Fact]
        public void ExtractIMUScaleFactorAccelerometerTest4()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x3C;
            int range = ExtractIMUScaleFactorAccelerometer(bytes);

            Assert.Equal(2048, range);
        }

        /// <summary>
        /// Tests the method ExtractIMURangeGyroscope
        /// </summary>
        [Fact]
        public void ExtractIMURangeGyroscopeTest1()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x29;
            double range = ExtractIMURangeGyroscope(bytes);

            Assert.Equal(500, range);
        }

        /// <summary>
        /// Tests the method ExtractIMURangeGyroscope
        /// </summary>
        [Fact]
        public void ExtractIMURangeGyroscopeTest2()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x24;
            double range = ExtractIMURangeGyroscope(bytes);

            Assert.Equal(250, range);
        }

        /// <summary>
        /// Tests the method ExtractIMURangeGyroscope
        /// </summary>
        [Fact]
        public void ExtractIMURangeGyroscopeTest3()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x34;
            double range = ExtractIMURangeGyroscope(bytes);

            Assert.Equal(1000, range);
        }

        /// <summary>
        /// Tests the method ExtractIMURangeGyroscope
        /// </summary>
        [Fact]
        public void ExtractIMURangeGyroscopeTest4()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x3C;
            double range = ExtractIMURangeGyroscope(bytes);

            Assert.Equal(2000, range);
        }

        /// <summary>
        /// Test the method ExctractIMUScaleFactorGyroscope
        /// </summary>
        [Fact]
        public void ExctractIMUScaleFactorGyroscopeTest1()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x29;
            double range = ExctractIMUScaleFactorGyroscope(bytes);

            Assert.Equal(65.5, range);
        }

        /// <summary>
        /// Test the method ExctractIMUScaleFactorGyroscope
        /// </summary>
        [Fact]
        public void ExctractIMUScaleFactorGyroscopeTest2()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x24;
            double range = ExctractIMUScaleFactorGyroscope(bytes);

            Assert.Equal(131, range);
        }

        /// <summary>
        /// Test the method ExctractIMUScaleFactorGyroscope
        /// </summary>
        [Fact]
        public void ExctractIMUScaleFactorGyroscopeTest3()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x34;
            double range = ExctractIMUScaleFactorGyroscope(bytes);

            Assert.Equal(32.8, range);
        }

        /// <summary>
        /// Test the method ExctractIMUScaleFactorGyroscope
        /// </summary>
        [Fact]
        public void ExctractIMUScaleFactorGyroscopeTest4()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x3C;
            double range = ExctractIMUScaleFactorGyroscope(bytes);

            Assert.Equal(16.4, range);
        }
    }
}
