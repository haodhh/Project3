using System;

namespace Project3.Common
{
    [Serializable]
    public class UserLogin
    {
        public string UserName { set; get; }
        public int Level { set; get; }
    }
}