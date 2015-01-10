using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ARES.Forms
{
    /// <summary>
    /// Provides access to forms that have been opened in the application.
    /// </summary>
    static class FormList
    {
        #region Attribute

        static private List<Form> formList = new List<Form>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Get form with specific type at the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public T Get<T>() where T : Form
        {
            foreach (Form item in formList)
            {
                if (item.GetType().Equals(typeof(T)))
                {
                    return (T)item;
                }
            }

            return null;
        }

        /// <summary>
        /// Add a new opened form to list.
        /// </summary>
        /// <param name="formReference"></param>
        static public void Add(Form form)
        {
            formList.Add(form);
        }

        /// <summary>
        /// Remove an existing form.
        /// </summary>
        static public void Remove(Form form)
        {
            if (formList.Contains(form))
            {
                formList.Remove(form);
            }
        }

        /// <summary>
        /// Remove an existing with given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static public void Remove<T>() where T : Form
        {
            Form form = null;
            foreach (Form item in formList)
            {
                if (item.GetType().Equals(typeof(T)))
                {
                    form = item;
                    break;
                }
            }

            if (form != null)
            {
                formList.Remove(form);
            }
        }

        /// <summary>
        /// Indicates whether the given form exists in the list.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        static public bool Exists(Form form)
        {
            return formList.Contains(form);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public bool Exists<T>() where T : Form
        {
            foreach (Form item in formList)
            {
                if (item.GetType().Equals(typeof(T)))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
