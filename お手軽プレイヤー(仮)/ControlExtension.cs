using System;
using System.Linq;
using System.Windows.Forms;

namespace LightPlayer
{
    public static class ControlExtension
    {
        /// <summary>
        /// コントロールの名前の末尾からインデックスを取得する
        /// </summary>
        public static int Index( this Control sender )
             => Convert.ToInt32( sender.Name.Split( '_' ).Last() );
    }
}