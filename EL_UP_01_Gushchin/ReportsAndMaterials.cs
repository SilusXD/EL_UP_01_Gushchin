//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EL_UP_01_Gushchin
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportsAndMaterials
    {
        public int id_report { get; set; }
        public int id_material { get; set; }
        public int count { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual Reports Reports { get; set; }
    }
}