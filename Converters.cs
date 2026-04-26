// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;

namespace Worldit.TreeView
{
	public class CollapsedWhenFalse : MarkupExtension, IValueConverter
	{
		public static CollapsedWhenFalse Instance = new CollapsedWhenFalse();

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Returns DependencyProperty.UnsetValue when the bound value is null,
	/// allowing the property to fall back to its inherited/default value.
	/// </summary>
	public class NullToUnsetConverter : MarkupExtension, IValueConverter
	{
		public static NullToUnsetConverter Instance = new NullToUnsetConverter();

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value ?? DependencyProperty.UnsetValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class TreeNodeForegroundConverter : MarkupExtension, IMultiValueConverter
	{
		public static TreeNodeForegroundConverter Instance = new TreeNodeForegroundConverter();

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var treeForeground = AsBrush(values, 0);
			var nodeForeground = AsBrush(values, 1);
			var selectedForeground = AsBrush(values, 2);
			var disabledForeground = AsBrush(values, 3);
			var isSelected = AsBool(values, 4);
			var isEnabled = AsBool(values, 5, true);

			if (!isEnabled && disabledForeground != null)
				return disabledForeground;

			if (isSelected && selectedForeground != null)
				return selectedForeground;

			return treeForeground ?? nodeForeground ?? Brushes.White;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		static Brush AsBrush(object[] values, int index)
		{
			return index < values.Length ? values[index] as Brush : null;
		}

		static bool AsBool(object[] values, int index, bool defaultValue = false)
		{
			return index < values.Length && values[index] is bool value ? value : defaultValue;
		}
	}

	public class TreeNodeTextBackgroundConverter : MarkupExtension, IMultiValueConverter
	{
		public static TreeNodeTextBackgroundConverter Instance = new TreeNodeTextBackgroundConverter();

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var selectedBackground = values.Length > 0 ? values[0] as Brush : null;
			var isSelected = values.Length > 1 && values[1] is bool value && value;
			return isSelected ? selectedBackground ?? SystemColors.HighlightBrush : null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class TreeItemBackgroundConverter : MarkupExtension, IMultiValueConverter
	{
		public static TreeItemBackgroundConverter Instance = new TreeItemBackgroundConverter();

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var background = values.Length > 0 ? values[0] as Brush : null;
			var alternationIndex = values.Length > 1 && values[1] is int index ? index : 0;
			var showAlternation = values.Length > 2 && values[2] is bool value && value;
			var alternationBackground = values.Length > 3 ? values[3] as Brush : null;
			var isMouseOver = values.Length > 4 && values[4] is bool mouseOver && mouseOver;
			var hoverBackground = values.Length > 5 ? values[5] as Brush : null;

			if (isMouseOver && hoverBackground != null)
				return hoverBackground;

			if (showAlternation && alternationIndex == 1)
				return alternationBackground ?? background ?? Brushes.Transparent;

			return background ?? Brushes.Transparent;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class GridItemBackgroundConverter : MarkupExtension, IMultiValueConverter
	{
		public static GridItemBackgroundConverter Instance = new GridItemBackgroundConverter();

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var background = values.Length > 0 ? values[0] as Brush : null;
			var selectedBackground = values.Length > 1 ? values[1] as Brush : null;
			var inactiveSelectedBackground = values.Length > 2 ? values[2] as Brush : null;
			var isSelected = values.Length > 3 && values[3] is bool selected && selected;
			var isSelectionActive = values.Length > 4 && values[4] is bool active && active;

			if (isSelected)
				return isSelectionActive
					? selectedBackground ?? SystemColors.HighlightBrush
					: inactiveSelectedBackground ?? SystemColors.ControlBrush;

			return background ?? Brushes.Transparent;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class GridItemForegroundConverter : MarkupExtension, IMultiValueConverter
	{
		public static GridItemForegroundConverter Instance = new GridItemForegroundConverter();

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var foreground = values.Length > 0 ? values[0] as Brush : null;
			var selectedForeground = values.Length > 1 ? values[1] as Brush : null;
			var inactiveSelectedForeground = values.Length > 2 ? values[2] as Brush : null;
			var disabledForeground = values.Length > 3 ? values[3] as Brush : null;
			var isSelected = values.Length > 4 && values[4] is bool selected && selected;
			var isSelectionActive = values.Length > 5 && values[5] is bool active && active;
			var isEnabled = values.Length > 6 && values[6] is bool enabled && enabled;

			if (!isEnabled)
				return disabledForeground ?? SystemColors.GrayTextBrush;

			if (isSelected)
				return isSelectionActive
					? selectedForeground ?? SystemColors.HighlightTextBrush
					: inactiveSelectedForeground ?? SystemColors.ControlTextBrush;

			return foreground ?? SystemColors.ControlTextBrush;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
