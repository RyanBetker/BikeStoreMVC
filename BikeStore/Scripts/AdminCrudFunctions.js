function IsNullEmptyOrWhitespace(input)
{
    return input === null || input == '' || input.match("/^\s*$/") !== null;
}

$(document).ready(function () {

    var ddlBrandName = $('#ddlBrandName');
    function AddBrandProcess()
    {
        //if value is blank, give popup to add brand, then append to list
        var newBrandName = prompt('Enter new brand name', '');
        
        if (!IsNullEmptyOrWhitespace(newBrandName)) {            
            $.ajax({
                url: '/Brand/Add',
                data: "{'BrandName' : '" + newBrandName + "', 'fromClientPage': 'true'}",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    $('<option value=' + result + '>' + newBrandName + '</option>').appendTo(ddlBrandName);
                    $('#ddlBrandName option:last-child').attr('selected', 'selected');
                },
                error: function (jqXHR, errorType, ex) { alert('error occured: ' + errorType + '. Details: ' + ex); }
            });
        }
        else {
            alert('Please retry and enter the new brand name that you\'d like added');
        }
    }
    $('#btnAddBrand').click(AddBrandProcess);
});