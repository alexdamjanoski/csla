﻿using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using LambdaRules.Properties;

namespace LambdaRules
{
  [Serializable]
  public class Root : BusinessBase<Root>
  {
    #region Business Methods

    public static readonly PropertyInfo<int> CustomerIdProperty = RegisterProperty<int>(c => c.CustomerId);
    public int CustomerId
    {
      get { return GetProperty(CustomerIdProperty); }
      set { SetProperty(CustomerIdProperty, value); }
    }


    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
    public string Name
    {
      get { return GetProperty(NameProperty); }
      set { SetProperty(NameProperty, value); }
    }

    public static readonly PropertyInfo<int> Num1Property = RegisterProperty<int>(c => c.Num1);
    public int Num1
    {
      get { return GetProperty(Num1Property); }
      set { SetProperty(Num1Property, value); }
    }


    public static readonly PropertyInfo<int> Num2Property = RegisterProperty<int>(c => c.Num2);
    public int Num2
    {
      get { return GetProperty(Num2Property); }
      set { SetProperty(Num2Property, value); }
    }

    #endregion

    #region Validation Rules

    protected override void AddBusinessRules()
    {
      // call base class implementation to add data annotation rules to BusinessRules 
      base.AddBusinessRules();


      // Lambd rule extensions are generic in order to give intellisense and strong typed lambda expression.
      // The lambda expression must return false when rule is broken. 
      BusinessRules.AddRule<Root>(NameProperty, o => !string.IsNullOrEmpty(o.Name), "Name is required");
      BusinessRules.AddRule<Root>(Num1Property, o => o.Num1 > 3, "Num1 must be larger than 3");
      BusinessRules.AddRule<Root>(Num2Property, o => o.Num2 > o.Num1, () => Resources.Num2LargerThanNum1, RuleSeverity.Warning);

      // Add dependency to recheck ruled for Num2 when Num1 is changed 
      BusinessRules.AddRule(new Dependency(Num1Property, Num2Property));   
    }

    #endregion

    #region Factory Methods

    public static Root NewEditableRoot()
    {
      return DataPortal.Create<Root>();
    }


    #endregion

  }
}