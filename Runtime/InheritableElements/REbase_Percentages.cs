using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// This partial is used to get percentages values to subobjects, for example when the parent object is 100 with a particular
    /// child must be 50, but if the parent is 400 child must be 200, use this functions to get this value easily inside the class
    /// First call SetReferenceSize() with a new instance of the setter for the object, then you can use GetPercentageSize() to get 
    /// the values, the function SetRealSize() is autocalled before AddComponents().
    /// </summary>
    public abstract partial class REbase
    {

        #region Properties

        // Real size of the object
        private float width;
        private float heigh;

        // Reference, or ideal size
        private float referenceWidth;
        private float referenceHeigh;

        #endregion Properties

        /// <summary>
        /// Must be set in AddComponents() of each component that use GetPercentagesSize()
        /// </summary>
        /// <param name="setter">A new created setter of the particular component</param>
        protected void SetReferenceSize(RectTransformBSetter setter)
        {
            referenceWidth = setter.width;
            referenceHeigh = setter.height;
        }

        /// <summary>
        /// Set in DrawRoot of all REbase
        /// </summary>
        /// <param name="setter">Setter of the object used to set the RectTransform</param>
        private void SetRealSize(RectTransformBSetter setter)
        {
            width = setter.width;
            heigh = setter.height;
        }

        /// <summary>
        /// Use to get a Vector2 with reference values, only work if SetReferenceSize() was called before
        /// </summary>
        /// <param name="percX">Ideal value for x when RealSize is equal to ReferenceSize</param>
        /// <param name="percY">Ideal value for y when RealSize is equal to ReferenceSize</param>
        /// <returns></returns>
        protected Vector2 GetPercentageSize(float percX, float percY)
        {
            return new Vector2((width * percX) / referenceWidth, (heigh * percY) / referenceHeigh);
        }


    }
}
