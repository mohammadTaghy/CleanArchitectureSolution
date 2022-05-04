using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Common.Test
{
    public class MemoryCacheManager_Test
    {
        private readonly IMemoryCacheManager _memoryCacheManager;

        public MemoryCacheManager_Test()
        {
            _memoryCacheManager = new MemoryCacheManager(new MemoryCache(new MemoryCacheOptions()));
        }
        [Fact]
        public void AddAsync_TackNullKey_ResultExceptionArgument()
        {
            Task<ArgumentNullException> exception = Assert.ThrowsAsync<ArgumentNullException>(
                () => _memoryCacheManager.AddAsync<string>(null,"Test Null"));
            Assert.Equal("key", exception.Result.ParamName);
        }
        [Fact]
        public void AddAsync_TackeKeyAndValue_ResultTest()
        {
            string value ="TestValue", key= "TestKey";
            _memoryCacheManager.AddAsync<string>(key, value);
            string value2 = _memoryCacheManager.GetWithKey<string>(key);
            Assert.Equal(value, value2);
        }
        [Fact]
        public void AddAsyncWithTimeout_TackNullKey_ResultExceptionArgument()
        {
            Task<ArgumentNullException> exception = Assert.ThrowsAsync<ArgumentNullException>(
                () => _memoryCacheManager.AddAsync<string>(null, "Test Null",0));
            Assert.Equal("key", exception.Result.ParamName);
        }
        [Theory]
        [MemberData(nameof(MemoryCacheTestData.MemoryCacheTestDataProp), MemberType = typeof(MemoryCacheTestData))]
        public void AddAsyncWithTimeout_CheckWithKeyValueExpierDate_ResultTest(string key,string value,int expierTime,string expected)
        {
           
            _memoryCacheManager.AddAsync<string>(key, value,expierTime);
            string value2 = _memoryCacheManager.GetWithKey<string>(key);
            Assert.Equal(expected, value2);
        }
        [Fact]
        public void Add_TackNullKey_ResultExceptionArgument()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => _memoryCacheManager.Add<string>(null, "Test Null"));
            Assert.Equal("key", exception.ParamName);
        }
        [Fact]
        public void Add_TackeKeyAndValue_ResultTest()
        {
            string value = "TestValue", key = "TestKey";
            _memoryCacheManager.Add<string>(key, value, 10);
            string value2 = _memoryCacheManager.GetWithKey<string>(key);
            Assert.Equal(value, value2);
        }
        [Fact]
        public void AddWithTimeout_TackNullKey_ResultExceptionArgument()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => _memoryCacheManager.Add<string>(null, "Test Null", 0));
            Assert.Equal("key", exception.ParamName);
        }
        [Theory]
        [MemberData(nameof(MemoryCacheTestData.MemoryCacheTestDataProp),MemberType =typeof(MemoryCacheTestData))]
        public void AddWithTimeout_CheckWithKeyValueExpierDate_ResultTest(string key, string value, int expierTime, string expected)
        {
           
            _memoryCacheManager.Add<string>(key, value, expierTime);
            string value2 = _memoryCacheManager.GetWithKey<string>(key);
            Assert.Equal(expected, value2);
        }

    }
    public static class MemoryCacheTestData
    {
        public static IEnumerable<object[]> MemoryCacheTestDataProp
        {
            get
            {
                yield return new object[] { "TestKey", "TestValue", 10, "TestValue" };
                yield return new object[] { "TestKey", "TestValue", 0, null };
            }
        }
    }
}
