//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ForrestGump.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServicesOrder
    {
        public int Id { get; set; }
        public Nullable<int> Id_Order { get; set; }
        public Nullable<int> Id_Service { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Service Service { get; set; }
    }
}
