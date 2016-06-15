using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Parking
{
    public class Row : INotifyPropertyChanged
    {
        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        /// <param name="leftInput">The left input.</param>
        /// <param name="rightInput">The right input.</param>
        ///////////////////////////////////////////////////////////////
        public Row(bool[] leftInput, bool[] rightInput)
        {
            for (int i = 0; i < leftInput.Length; i++)
            {
                leftSpaces.Add(leftInput[i]);
                middleSpaces.Add(false);
                rightSpaces.Add(rightInput[i]);
            }
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        /// <param name="length">The length.</param>
        ///////////////////////////////////////////////////////////////
        public Row(int length)
        {
            for (int i = 0; i < length; i++)
            {
                leftSpaces.Add(false);
                middleSpaces.Add(false);
                rightSpaces.Add(false);
            }
        }

        public List<bool> leftSpaces { get; set; }
        public List<bool> rightSpaces { get; set; }
        public List<bool> middleSpaces { get; set; }

        #region Notify Property Changed Members

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
