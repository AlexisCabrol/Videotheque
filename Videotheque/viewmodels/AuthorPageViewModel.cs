﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Videotheque.config;
using Videotheque.models;
using Videotheque.services.personne;
using Videotheque.services.personne.impl;
using Videotheque.views;

namespace Videotheque.viewmodels
{
    class AuthorPageViewModel : AbstractModel
    {
        private readonly PersonneService personneService = new PersonneServiceImpl();
        public MainViewModel SuperViewModel { get { return GetValue<MainViewModel>(); } set { SetValue<MainViewModel>(value); } }
        public ObservableCollection<Personne> Authors { get { return GetValue<ObservableCollection<Personne>>(); } set { SetValue<ObservableCollection<Personne>>(value); }}
        public Personne CurrentAuthor { get { return GetValue<Personne>(); } set { SetValue<Personne>(value); } }

        public AuthorPageViewModel(MainViewModel mvm)
        {
            SuperViewModel = mvm;
        }

        public async void CallService()
        {
            Authors = await personneService.SelectAllCollab();
        }

        public async void SearchByText(string text)
        {
            Authors = await personneService.SelectCollabFilter(text);
        }

        public Command ConsultAuthor
        {
            get
            {
                return new Command(() =>
                {
                    SuperViewModel.MVMAuthor = CurrentAuthor;
                    SuperViewModel.Source = NavigationCache.GetPage<ConsultAuthorPage, ConsultAuthorPageViewModel>(SuperViewModel);
                });
            }
        }

        public Command AddAuthor
        {
            get
            {
                return new Command(() =>
                {
                    SuperViewModel.MVMAuthor = null;
                    SuperViewModel.Source = NavigationCache.GetPage<AddAuthorPage, AddAuthorPageViewModel>(SuperViewModel);
                });
            }
        }

        public Command GoBack
        {
            get
            {
                return new Command(() =>
                {
                    SuperViewModel.Source = NavigationCache.GetPage<HomePage, HomePageViewModel>(SuperViewModel);
                });
            }
        }
    }
}
