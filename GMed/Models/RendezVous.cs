//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GMed.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class RendezVous
    {
        public int RendezVousId { get; set; }


        public int ActeMedicalId { get; set; }
        
        public virtual ActeMedical ActeMedical { get; set; }

       

        
        public string DateRendezVous { get; set; }

        
        public string AdresseRendezVous { get; set; }
    }
}