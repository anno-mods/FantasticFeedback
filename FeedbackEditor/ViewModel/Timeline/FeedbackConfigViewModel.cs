﻿using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.Services;
using FeedbackEditor.ViewModel.Timeline;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeLines;

namespace FeedbackEditor.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class FeedbackConfigViewModel : TimeLinesDataBase, IChannel
    {
        public string ChannelName 
        { 
            get => FcFileService.Instance.CurrentFile.GetActorName(FeedbackConfig) ?? String.Empty;
            set => FcFileService.Instance.CurrentFile.TrySetActorName(FeedbackConfig, value);
        }
        public Thickness OffsetOverride => new Thickness(20, 3, 3, 3);
        public ChannelType ChannelType { get; } = ChannelType.ACTOR;

        public ObservableCollection<SequenceDefinitionViewModel> SequenceDefinitions { get; } = new(); 

        public FeedbackConfig FeedbackConfig { get; }

        private DummyGroup? _multiplyActorByDummyCount;
        public DummyGroup? MultiplyActorByDummyCount 
        {
            get => _multiplyActorByDummyCount;
            set 
            {
                _multiplyActorByDummyCount = value;
                FeedbackConfig.MultiplyActorByDummyCount = value?.Name ?? String.Empty;
            }
        
        }

        [AddINotifyPropertyChangedInterface]
        public class GuidVariation
        {
            public int Guid { get; set; }
        }

        public ObservableCollection<GuidVariation> GuidVariations { get; }

        public class FeedbackLoopEntry
        {
            public FeedbackSequenceType ParentSequence { get; set; }
            public SequenceDefinitionViewModel? Plays { get; set; }
        }

        public ObservableCollection<FeedbackLoopEntry> FeedbackLoops { get; private set; }

        public FeedbackConfigViewModel(FeedbackConfig feedbackConfig)
        {
            FeedbackConfig = feedbackConfig;
            _multiplyActorByDummyCount = FcFileService.Instance.GetDummyGroup(FeedbackConfig.MultiplyActorByDummyCount);
            var index = 0;
            foreach (var sequenceDefinition in feedbackConfig.SequenceDefinitions)
            {
                var viewModel = new SequenceDefinitionViewModel(sequenceDefinition);
                viewModel.ChannelName = "Sequence " + index;
                Childs.Add(viewModel);
                SequenceDefinitions.Add(viewModel);
                index++;
            }

            GuidVariations = new ObservableCollection<GuidVariation>(
                FeedbackConfig
                .AssetVariationList?
                .GuidVariationList
                .Select(x => new GuidVariation { Guid = x.Item1 })

                ?? Enumerable.Empty<GuidVariation>());
            InitFeedbackLoops();
        }

        public void InitFeedbackLoops()
        {
            FeedbackLoops = new ObservableCollection<FeedbackLoopEntry>(
                FcFileService
                .Instance
                .CurrentFile
                .FeedbackDefinition
                .ValidSequenceIDs
                .OrderBy(x => x)
                .Select(x => {
                    bool supportsSequence = FeedbackConfig.FeedbackLoops.ReadonlyValues.ContainsKey(x);
                    return new FeedbackLoopEntry()
                    {
                        ParentSequence = x,
                        Plays = supportsSequence ?
                            SequenceDefinitions.ElementAt(
                                FeedbackConfig
                                .FeedbackLoops
                                .ReadonlyValues
                                .GetValueOrDefault(x))
                            : null
                    };
                }));
        }

        public void AddActor(int actorGuid)
        {
            GuidVariations.Add(new GuidVariation { Guid = actorGuid });
            UpdateModelGuidVariations();
        }

        public void RemoveActor(GuidVariation variation)
        {
            GuidVariations.Remove(variation);
            UpdateModelGuidVariations();
        }

        public void UpdateModelGuidVariations()
        {
            if (FeedbackConfig?.AssetVariationList is null)
                return;
            FeedbackConfig.AssetVariationList.GuidVariationList
                = GuidVariations.Select(x => (x.Guid, -1)).ToList();
        }

        public void UpdateModelFeedbackLoops()
        {
            FeedbackConfig.FeedbackLoops.Clear();
            foreach (var feedbackLoopEntry in FeedbackLoops)
            {
                if (feedbackLoopEntry.Plays is null)
                    continue;
                FeedbackConfig.FeedbackLoops.SetValue(feedbackLoopEntry.ParentSequence, SequenceDefinitions.IndexOf(feedbackLoopEntry.Plays));
            }
        }

        public void AddSequenceDefinition(SequenceDefinitionViewModel sequenceDefinitionViewModel)
        {
            Childs.Add(sequenceDefinitionViewModel);
            SequenceDefinitions.Add(sequenceDefinitionViewModel);
        }

        public void RemoveSequenceDefinition(SequenceDefinitionViewModel sequenceDefinitionViewModel)
        {
            FeedbackConfig.RemoveSequenceDefinition(sequenceDefinitionViewModel.SequenceDefinition);
            Childs.Remove(sequenceDefinitionViewModel);
            SequenceDefinitions.Remove(sequenceDefinitionViewModel);
        }
    }
}
