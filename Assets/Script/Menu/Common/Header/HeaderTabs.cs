﻿using System;
using System.Collections.Generic;
using UnityEngine;
using YARG.Helpers.Extensions;
using YARG.Menu.Navigation;

namespace YARG.Menu
{
    [RequireComponent(typeof(NavigationGroup))]
    public class HeaderTabs : MonoBehaviour
    {
        [Serializable]
        public struct TabInfo
        {
            public Sprite Icon;
            public string Id;

            // TODO: Localize this
            public string DisplayName;
        }

        [SerializeField]
        private GameObject _tabPrefab;
        [SerializeField]
        private List<TabInfo> _tabs;
        public List<TabInfo> Tabs
        {
            get => _tabs;
            set
            {
                _tabs = value;
                RefreshTabs();
            }
        }

        private string _selectedTabId;
        public string SelectedTabId
        {
            get => _selectedTabId;
            set
            {
                _selectedTabId = value;
                TabChanged?.Invoke(value);
            }
        }

        public event Action<string> TabChanged;

        private NavigationGroup _navigationGroup;

        private void Awake()
        {
            _navigationGroup = GetComponent<NavigationGroup>();
            _navigationGroup.SelectionChanged += OnSelectionChanged;
        }

        private void Start()
        {
            RefreshTabs();
        }

        private void OnSelectionChanged(NavigatableBehaviour obj)
        {
            TabChanged?.Invoke(SelectedTabId);
        }

        public void RefreshTabs()
        {
            transform.DestroyChildren();
            _navigationGroup.ClearNavigatables();

            foreach (var tabInfo in _tabs)
            {
                var tab = Instantiate(_tabPrefab, transform);

                var tabComponent = tab.GetComponent<HeaderTab>();
                tabComponent.Init(this, tabInfo.Id, tabInfo.DisplayName, tabInfo.Icon);

                _navigationGroup.AddNavigatable(tabComponent);
            }

            _navigationGroup.SelectFirst();
        }

        private void OnDestroy()
        {
            _navigationGroup.SelectionChanged -= OnSelectionChanged;
        }
    }
}