using System;
namespace Telegraph.Tests
{
    public class Pokemon
    {
        #region Properties

        public long Id { get; set; }
        public string Name { get; set; }
        public long BaseExperience { get; set; }
        public long Height { get; set; }
        public bool IsDefault { get; set; }
        public long Order { get; set; }
        public long Weight { get; set; }

        #endregion

        #region Constructor

        public Pokemon()
        {

        }

        #endregion
    }
}