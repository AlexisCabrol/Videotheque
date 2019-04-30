﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Videotheque.models.viewmodels
{
    class MainViewModel : AbstractModel
    {
        public Page Source
        {
            get { return GetValue<Page>(); }
            set { SetValue<Page>(value); }
        }
    }
}
