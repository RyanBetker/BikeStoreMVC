
$(document).ready(function () {

    var ddlBrandName = $('#ddlBrandName');

    var fnOnBrandChange = function () {
        var selectedBrandID = ddlBrandName.val();
        
        if (selectedBrandID == "")
        {            
            //if value is blank, give popup to add brand, then append to list
            var newBrandName = prompt('Enter new brand name', '', '-');
            $('<option value=-1>' + newBrandName + '</option>').appendTo(ddlBrandName);
            $('#ddlBrandName option:last-child').attr('selected', 'selected');

        }
    };

    $('#ddlBrandName').change(fnOnBrandChange);

    //http://andyck1.blogspot.com/2012/06/aspnet-dropdownlist-postback-jquery.html
});