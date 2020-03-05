using EarablesKIT.Models.Library;
using System;
using Xunit;
using static EarablesKIT.Models.Library.IMUDataExtractor;


namespace XUnitTest
{
    public class IMUDataExtractorTest
    {
        [Fact]
        public void ExtractStringTest()
        {
            byte[] byteIMUData = new byte[16] { 0x55, 1, 42, 0x0C, 0b_00000000, 0b_11000101, 0b_00000011,
            0b_01101100, 0b_00000011, 0b_11100111, 0b_00100000, 0b_10100001, 0b_10011110, 0b00011100, 0b_00000100, 0b_00000110 };
            double accScaleFactor = 8192;
            double gyroScaleFactor = 65.5;
            byte[] offset = new byte[15];
            IMUDataEntry entry = ExtractIMUDataString(byteIMUData, accScaleFactor, gyroScaleFactor, offset);

            Assert.Equal((10/9.80665) , (double)entry.Acc.G_X, 2);
            Assert.Equal(-30/9.80665, (double)entry.Acc.G_Y, 2);
            Assert.Equal(1.234/9.80665, (double)entry.Acc.G_Z, 2);

            Assert.Equal(10, (double)entry.Acc.MperS_X, 2);
            Assert.Equal(-30, (double)entry.Acc.MperS_Y, 2);
            Assert.Equal(1.234, (double)entry.Acc.MperS_Z, 2);

            Assert.Equal(3.007, (double)entry.Gyro.DegsPerSec_X, 2);
            Assert.Equal(13.37, (double)entry.Gyro.DegsPerSec_Y, 2);
            Assert.Equal(15.25, (double)entry.Gyro.DegsPerSec_Z, 2);
        }

        [Fact]
        public void ExtractIMURangeAccelerometerTest()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x29;
            int range = ExtractIMURangeAccelerometer(bytes);

            Assert.Equal(4, range);
        }

        [Fact]
        public void ExtractIMUScaleFactorAccelerometerTest()
        {
            byte[] bytes = new byte[6];
            bytes[5] = 0x29;
            int range = ExtractIMUScaleFactorAccelerometer(bytes);

            Assert.Equal(8192, range);
        }

        [Fact]
        public void ExtractIMURangeGyroscopeTest()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x29;
            double range = ExtractIMURangeGyroscope(bytes);

            Assert.Equal(500, range);
        }

        [Fact]
        public void ExctractIMUScaleFactorGyroscopeTest()
        {
            byte[] bytes = new byte[6];
            bytes[4] = 0x29;
            double range = ExctractIMUScaleFactorGyroscope(bytes);

            Assert.Equal(65.5, range);
        }
    }
}
