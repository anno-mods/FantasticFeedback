﻿using FeedbackEditor.ViewModel;
using FeedbackEditor.ViewModel.Dummies;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FeedbackEditor.Views
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    /// 
    [AddINotifyPropertyChangedInterface]
    public partial class ActorSettingsView : UserControl
    {
        public FeedbackConfigViewModel? DisplayedActor
        {
            get => GetValue(DisplayedActorProperty) as FeedbackConfigViewModel;
            set => SetValue(DisplayedActorProperty, value);
        }

        public static readonly DependencyProperty DisplayedActorProperty = DependencyProperty.Register(
            nameof(DisplayedActor),
            typeof(FeedbackConfigViewModel),
            typeof(ActorSettingsView)
        );

        public ActorSettingsView()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void NumericSpinner_ValueChanged(object sender, EventArgs e)
        {

        }

        private void OnPreviewDropAcceptOnlyDummyGroups(object sender, DragEventArgs e)
        {
            e.Handled = e.Data.GetData(typeof(DummyGroupViewModel)) is DummyGroupViewModel;
        }

        private void OnDefaultStateDummyDropped(object sender, DragEventArgs e)
        {
            var vm = e.Data.GetData(typeof(DummyGroupViewModel)) as DummyGroupViewModel;

            if (vm != null && DisplayedActor is not null)
            {
                DisplayedActor.MultiplyActorByDummyCount = vm.DummyGroup;
            }
        }

        private void OnClearMultiplyByDummyClick(object sender, RoutedEventArgs e)
        {
            if (DisplayedActor is not null)
                DisplayedActor.MultiplyActorByDummyCount = null;
        }

        private void OnRemoveGuidClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;
            if (button.DataContext is not FeedbackConfigViewModel.GuidVariation variation)
                return;
            if (DisplayedActor is null)
                return;

            DisplayedActor.RemoveActor(variation);
        }

        private void OnAddGuidClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;
            if (DisplayedActor is null)
                return;

            DisplayedActor.AddActor(0);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
