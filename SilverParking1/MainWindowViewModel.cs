using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Parking
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Constructor

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        ///////////////////////////////////////////////////////////////
        public MainWindowViewModel()
        {
            //this.RowNum = 3;
            //this.RowLength = 10;
            this.Rows = new List<Row>(3);
            this.RMatrix = new int[0][];
            this.QMatrix = new int[0][];

            //this.DrawLot();

            DrawLotCommand = new RelayCommand(this.DrawLot);
            DrawLotCommand.IsEnabled = true;

            TrainCommand = new RelayCommand(this.Train);
            TrainCommand.IsEnabled = true;

            TestCommand = new RelayCommand(this.Test);
            TestCommand.IsEnabled = true;
        }

        #endregion

        #region Public Properties

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        ///////////////////////////////////////////////////////////////
        public List<Row> Rows
        {
            get { return this._rows; }
            set
            {
                if (this._rows != value)
                {
                    this._rows = value;
                    this.NotifyPropertyChanged("Rows");
                }
            }
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the row num.
        /// </summary>
        /// <value>
        /// The row num.
        /// </value>
        ///////////////////////////////////////////////////////////////
        public int RowNum
        {
            get { return this._rowNum; }
            set
            {
                if (this._rowNum != value)
                {
                    this._rowNum = value;
                    this.NotifyPropertyChanged("RowNum");
                }
            }
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the length of the row.
        /// </summary>
        /// <value>
        /// The length of the row.
        /// </value>
        ///////////////////////////////////////////////////////////////
        public int RowLength
        {
            get { return this._rowLength; }
            set
            {
                if (this._rowLength != value)
                {
                    this._rowLength = value;
                    this.NotifyPropertyChanged("RowLength");
                }
            }
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the learning rate.
        /// </summary>
        /// <value>
        /// The learning rate.
        /// </value>
        ///////////////////////////////////////////////////////////////
        public decimal LearningRate
        {
            get { return this._learningRate; }
            set
            {
                if (this._learningRate != value)
                {
                    this._learningRate = this.Limit(value);
                    this.NotifyPropertyChanged("LearningRate");
                }
            }
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        ///////////////////////////////////////////////////////////////
        public decimal Discount
        {
            get { return this._discount; }
            set
            {
                if (this._discount != value)
                {
                    this._discount = this.Limit(value);
                    this.NotifyPropertyChanged("Discount");
                }
            }
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// The R matrix
        /// </summary>
        ///////////////////////////////////////////////////////////////
        public int[][] RMatrix;

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// The Q matrix
        /// </summary>
        ///////////////////////////////////////////////////////////////
        public int[][] QMatrix;

        #endregion

        #region Public Methods

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the lot.
        /// </summary>
        ///////////////////////////////////////////////////////////////
        public void DrawLot()
        {            
            this.Rows.Clear();

            //for (int i = 0; i < this.RowNum; i++)
            for (int i = 0; i < 2; i++)
            {
                this.Rows.Add(new Row(this.RowLength));
            }

            //this.RowNum = 3;
            //this.RowLength = 10;
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Trains this instance.
        /// </summary>
        ///////////////////////////////////////////////////////////////
        public void Train()
        {
            int i, j, k;

            //
            // Resize the RMatrix and QMatrix according to the UI settings
            //
            this.RMatrix = new int[this.RowNum][];
            for (i = 0; i < this.RowNum; i++)
            {
                this.RMatrix[i] = new int[this.RowLength];
            }

            this.QMatrix = new int[this.RowNum][];
            for (i = 0; i < this.RowNum; i++)
            {
                this.QMatrix[i] = new int[this.RowLength];
            }

            //
            // Iterate through and set the Rewards
            //
            for (j = 0; j < this.RowNum; j++)
            {
                for (k = 0; k < this.RowLength; k++)
                {
                    if (this.Rows.ElementAt(j).leftSpaces.ElementAt(k) == true)
                    {
                        // Negative reward if occupied
                        this.RMatrix[j][k] = -5;
                    }
                    else
                    {
                        // Decreasing reward if open
                        this.RMatrix[j][k] = this.RowLength - k + 1;
                    }
                }
            }

            //this.TrainSystem(this.RMatrix, this.QMatrix);
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Tests this instance.
        /// </summary>
        ///////////////////////////////////////////////////////////////
        public void Test()
        {

        }

        #endregion

        #region Private Helper Methods

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Limits the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        ///////////////////////////////////////////////////////////////
        private decimal Limit(decimal value)
        {
            if (value.CompareTo(0) == -1)
                return 0;
            if (value.CompareTo(1) == 1)
                return 1;
            return Math.Round(value, 5);
        }

        #endregion

        #region Event Handlers

        public RelayCommand DrawLotCommand { get; private set; }
        public RelayCommand TrainCommand { get; private set; }
        public RelayCommand TestCommand { get; private set; }

        #endregion

        #region Private Properties

        private int _rowNum;
        private int _rowLength;
        private decimal _discount;
        private decimal _learningRate;
        private List<Row> _rows;

        #endregion

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
