﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Point = System.Windows.Point;

namespace PiP_Tool.ViewModels
{
    public class Selector : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Point _selectorBoxPosition = new Point(100, 200);
        private Size _selectorBoxSize = new Size(50, 300);
        private bool _dragging = false;

        public Point SelectorBoxPosition
        {
            get { return _selectorBoxPosition; }
            set
            {
                _selectorBoxPosition = value;
                NotifyPropertyChanged();
            }
        }

        public Size SelectorBoxSize
        {
            get { return _selectorBoxSize; }
            set
            {
                _selectorBoxSize = value;
                NotifyPropertyChanged();
            }
        }

        public void SetCursor(Point mousePosition)
        {
            var left = _selectorBoxPosition.X;
            var top = _selectorBoxPosition.Y;
            var right = left + _selectorBoxSize.Width;
            var bottom = top + _selectorBoxSize.Height;
            
            var margin = 10;
            if ((mousePosition.X + margin) < right && (mousePosition.X - margin) > left &&
                (mousePosition.Y + margin) < bottom && (mousePosition.Y - margin) > top)
            {
                Mouse.OverrideCursor = Cursors.ScrollAll;
            }
            else if ((Math.Abs(mousePosition.X - left) < margin && Math.Abs(mousePosition.Y - bottom) < margin) ||
                (Math.Abs(mousePosition.X - right) < margin && Math.Abs(mousePosition.Y - top) < margin))
            {
                Mouse.OverrideCursor = Cursors.SizeNESW;
            }
            else if ((Math.Abs(mousePosition.X - right) < margin && Math.Abs(mousePosition.Y - bottom) < margin) ||
                (Math.Abs(mousePosition.X - left) < margin && Math.Abs(mousePosition.Y - top) < margin))
            {
                Mouse.OverrideCursor = Cursors.SizeNWSE;
            }
            else if (Math.Abs(mousePosition.X - right) < margin || Math.Abs(mousePosition.X - left) < margin)
            {
                Mouse.OverrideCursor = Cursors.SizeWE;
            }
            else if (Math.Abs(mousePosition.Y - top) < margin || Math.Abs(mousePosition.Y - bottom) < margin)
            {
                Mouse.OverrideCursor = Cursors.SizeNS;
            }
            else
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        public void MouseDown()
        {
            _dragging = true;
        }

        public void MouseUp()
        {
            _dragging = false;
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
