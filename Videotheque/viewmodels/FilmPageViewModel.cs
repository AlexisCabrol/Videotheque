﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videotheque.config;
using Videotheque.views;
using System.Collections.ObjectModel;
using Videotheque.models;
using Videotheque.services.film.impl;
using Videotheque.services.film;

namespace Videotheque.viewmodels
{
    class FilmPageViewModel : AbstractModel
    {
        private readonly FilmService filmService = new FilmServiceImpl();
        public MainViewModel SuperViewModel { get { return GetValue<MainViewModel>(); } set { SetValue<MainViewModel>(value); } }
        public FilmPageViewModel(MainViewModel mvm)
        {
            SuperViewModel = mvm;
        }

        public async void CallService()
        {
            this.Films = await filmService.SelectAllFilmAsync();
        }

        public async void SearchByText(string text)
        {
            Films = await filmService.SelectFilmFilter(text);
        }

        public ObservableCollection<Film> Films
        {
            get { return GetValue<ObservableCollection<Film>>();  }
            set { SetValue<ObservableCollection<Film>>(value);  }
        }

        public Film CurrentFilm
        {
            get { return GetValue<Film>(); }
            set { SetValue<Film>(value);  }
        }

        public async void DeleteFilm()
        {
            await filmService.DeleteFilm(CurrentFilm);
            CallService();
        }

        public Command ConsultFilm
        {
            get
            {
                return new Command(() =>
                {
                    SuperViewModel.MVMFilm = CurrentFilm;
                    SuperViewModel.Source = NavigationCache.GetPage<ConsultFilmPage, ConsultFilmPageViewModel> (SuperViewModel);
                });
            }
        }

        public Command AddFilm
        {
            get
            {
                return new Command(() =>
                {
                    SuperViewModel.MVMFilm = null;
                    SuperViewModel.Source = NavigationCache.GetPage<AddFilmPage, AddFilmPageViewModel>(SuperViewModel);
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
