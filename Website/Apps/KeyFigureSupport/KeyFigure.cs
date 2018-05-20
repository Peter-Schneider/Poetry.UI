using Poetry.UI.FormSupport;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Apps.KeyFigureSupport
{
    [Form("key-figure")]
    public class KeyFigure
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}