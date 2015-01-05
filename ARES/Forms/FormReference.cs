using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARES.Forms
{
    static class FormReference
    {
        #region Attribute

        private static GoToForm goToForm = null;
        private static EditExtentForm editExtentForm = null;
        //private static OptionForm optionForm = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the reference of the GoTo Form.
        /// </summary>
        public static GoToForm GoToForm
        {
            set { goToForm = value; }
            get { return goToForm; }
        }

        /// <summary>
        /// Gets or sets the reference of the Eidt Extent Form.
        /// </summary>
        public static EditExtentForm EditExtentForm
        {
            set { editExtentForm = value; }
            get { return editExtentForm; }
        }

        ///// <summary>
        ///// Gets or sets the reference of the Option Form.
        ///// </summary>
        //public static OptionForm OptionForm
        //{
        //    set { optionForm = value; }
        //    get { return optionForm; }
        //}

        #endregion
    }
}
