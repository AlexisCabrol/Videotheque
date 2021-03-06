﻿using System.Collections.Generic;
using Videotheque.config;
using Videotheque.models;
using Videotheque.models.enums;
using Videotheque.services.personne;
using Videotheque.services.personne.impl;
using Videotheque.utils;
using Videotheque.views;

namespace Videotheque.viewmodels
{
    class AddFriendPageViewModel : AbstractModel
    {
        private readonly PersonneService personneService = new PersonneServiceImpl();
        private bool UpdateMode = false;
        public MainViewModel SuperViewModel { get { return GetValue<MainViewModel>(); } set { SetValue<MainViewModel>(value); } }
        public Personne Friend { get { return GetValue<Personne>(); } set { SetValue<Personne>(value); } }
        public List<ComboboxUtils> ListCivilite { get; set; }
        public List<ComboboxUtils> ListNat { get; set; }

        public AddFriendPageViewModel(MainViewModel mvm)
        {
            SuperViewModel = mvm;
            ListCivilite = ComboboxUtils.init(new Civilite());
            ListNat = ComboboxUtils.init(new Pays());
        }

        public void ImageSetup(byte[] array)
        {
            Friend.PhotoProfil = array;
        }

        public void Setup()
        {
            if (SuperViewModel.MVMFriend != null)
            {
                Friend = SuperViewModel.MVMFriend;
                UpdateMode = true;
            }
            else
            {
                Friend = new Personne
                {
                    Ami = true
                };
            }
        }

        public Command ValidateFriend
        {
            get
            {
                return new Command(async () =>
                {
                    if (UpdateMode)
                    {
                        await personneService.UpdatePersonne(Friend);
                        SuperViewModel.MVMFriend = Friend;
                        SuperViewModel.Source = NavigationCache.GetPage<ConsultFriendPage, ConsultFriendPageViewModel>(SuperViewModel);
                    }
                    else
                    {
                        await personneService.AddPersonne(Friend);
                        SuperViewModel.Source = NavigationCache.GetPage<FriendPage, FriendPageViewModel>(SuperViewModel);
                    }
                });
            }
        }

        public Command GoBack
        {
            get
            {
                return new Command(() =>
                {
                    SuperViewModel.Source = NavigationCache.GetPage<FriendPage, FriendPageViewModel>(SuperViewModel);
                });
            }
        }
    }
}
