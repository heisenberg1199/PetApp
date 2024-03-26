namespace PetMan.Models
{
    public enum Species { dog, cat }
    public class Pet
    {
        
        private int _id = 1;
        /// <summary>
        /// primary key
        /// </summary>
        public int Id 
        { 
            get { return _id;} 
            set { if (value >= 1)  _id = value; } 
        }
        private string _nickname = "Unknow nickname";
        /// <summary>
        /// pet nickname
        /// </summary>
        public string NickName 
        { 
            get { return _nickname; }  
            set { if (!string.IsNullOrEmpty(value)) _nickname = value; } // not null value
        }
        private int _age = 1;
        /// <summary>
        /// age pet . Age > 0
        /// </summary>
        public int Age 
        { 
            get { return _age; } 
            set { if (value > 0) _age = value; }
        }
        /// <summary>
        /// pet species. Only accept species dog or cat
        /// </summary>
        public Species Species { get; set; } = Species.dog;
        private string _physicalDescription = "Unknow physical description";
         /// <summary>
        /// pet physical description 
        /// </summary>
        public string PhysicalDescription 
        { 
            get { return _physicalDescription; }
            set { if (!string.IsNullOrEmpty(value)) _physicalDescription = value; } // not null value
        }
        private string _personalityDescription = "Unknow personality description";
        /// <summary>
        /// pet personality description
        /// </summary>
        public string PersonalityDescription 
        { 
            get { return _personalityDescription; } 
            set { if (!string.IsNullOrEmpty(value)) _personalityDescription = value; } // not null value
        }
        public bool Adopt { get; set; } = false;
        /// <summary>
        /// pet image
        /// </summary>
        public string Image { get; set; } = "";
        public string FileReport { get; set; } = "";
    }
}