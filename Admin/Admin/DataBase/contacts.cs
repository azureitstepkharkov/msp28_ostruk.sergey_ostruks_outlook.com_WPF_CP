//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Admin.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class contacts
    {
        public int id { get; set; }
        public int type_id { get; set; }
        public string value { get; set; }
        public Nullable<int> user_id { get; set; }
    }
}
