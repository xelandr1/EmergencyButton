using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyButton.Core.ProcessFlow
{
    class Class1
    {

    }

    ///// <summary>
    ///// Базовый тип всех элементов метаданных процесса
    ///// </summary>
    //public abstract class DfMetaItem : IDfMetaItem
    //{
    //    private IDfMetaItemsContainer _parent;

    //    /// <summary>
    //    /// Заголовочное описание элемента
    //    /// </summary>
    //    public string DescriptionHeader { get; set; }

    //    /// <summary>
    //    /// Описание элемента
    //    /// </summary>
    //    public string Description { get; set; }

    //    /// <summary>
    //    /// элемент контейнер
    //    /// </summary>
    //    public IDfMetaItemsContainer Parent
    //    {
    //        get { return _parent; }
    //        set
    //        {
    //            if (_parent != null) throw new ArgumentException("Родительский элемент уже задан");
    //            _parent = value;
    //        }
    //    }

    //    /// <summary>
    //    /// Идентификатор элемента
    //    /// </summary>
    //    public Guid Id { get; set; }


    //    /// <summary>
    //    /// бизнес-правила состояний элемента
    //    /// </summary>
    //    public IBooleanRuleCollection<DfInstanceItemState> StateRules { get; private set; }

    //    ///// <summary>
    //    ///// Признак возможности создания нескольких инстансов элемента
    //    ///// </summary>
    //    //public bool MultiInstance { get; set; }

    //    /// <summary>
    //    /// Поведение инстанса операции
    //    /// </summary>
    //    public DfInstanceBehaviour InstanceBehaviour { get; set; }


    //    /// <summary>
    //    /// Входящие связи
    //    /// </summary>
    //    ///<remarks>Связи хранятся парами, для каждой входящей связи, должна быть копия в исходящих, связанного элемента</remarks>
    //    public List<DfLink> Inlinks { get; private set; }

    //    /// <summary>
    //    /// исходящие связи
    //    /// </summary>
    //    public List<DfLink> Outlinks { get; private set; }

    //    protected DfMetaItem()
    //    {
    //        InstanceBehaviour = DfInstanceBehaviour.SingleInstance;
    //        Outlinks = new List<DfLink>();
    //        Inlinks = new List<DfLink>();
    //        StateRules = new BooleanRuleCollection<DfInstanceItemState>();
    //    }
    //    public override string ToString()
    //    {
    //        return this.GetType().Name + DescriptionHeader;
    //    }

    //    public virtual bool ConfirmAction(IReadOnlyServiceProvider serviceProvider)
    //    {
    //        return true;
    //    }
    //}

}
