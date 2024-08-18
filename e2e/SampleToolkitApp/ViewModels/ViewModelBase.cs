﻿using Prism.Mvvm;

namespace SampleToolkitApp.ViewModels
{
  public class ViewModelBase : BindableBase
  {
    private string _title = string.Empty;

    public string Title { get => _title; set => SetProperty(ref _title, value); }
  }
}
