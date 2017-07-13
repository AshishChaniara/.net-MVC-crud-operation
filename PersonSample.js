var basepath = window.location.protocol + "//" + window.location.host + "/";
jQuery(document).ready(function () {
    jQuery("#btnSave").click(function () {
        var cityName = jQuery("#txt1").val();
        var stateID = jQuery("#txt2").val();
        var cityID = jQuery("#CityId").val();
        var errorMsg = "";
        if (cityName == "")
        {
            errorMsg += "Please Enter Name" + "\n";
        }
        if (cityID == "") {
            errorMsg += "Please Enter salary" + "\n";
        } 
        if (stateID == "") {
            errorMsg += "Please Enter stateID";
        }

        if (errorMsg == "") {

            jQuery.ajax({
                url: basepath + 'Action/Save'
                , data: { cityID: cityID, cityName: cityName, stateID: stateID }
                , type: 'POST'
                , dataType: 'JSON'
                , success: function (response) {
                        alert(response.responseText);
                }
            });
        } else {
            alert(errorMsg);
        }


    });

    jQuery("Delete").click(function () {
    });
    jQuery("#btnClear").click(function () {
        jQuery("#txt1").val("");
        jQuery("#txt2").val("");
        jQuery("#CityId").val("");
    });
});

function onSuccessbtnSave(response) {
    alert(response.responseText);
}

function Edit(value) {
    jQuery.ajax({
        url: basepath + 'Action/Edit'
        , data: { cityID: value}
        , type: 'POST'
        , dataType: 'JSON'
        , success: function (response) {
            jQuery("#txt1").val(response.CityName);
            jQuery("#txt2").val(response.StateID);
            jQuery("#CityId").val(response.CityID);
        }
    });
}

function Delete(value) {
    jQuery.ajax({
        url: basepath + 'Action/Delete'
        , data: { cityID: value }
        , type: 'POST'
        , dataType: 'JSON'
        , success: function (response) {
            alert(response.responseText);
            window.location.reload(true);
        }
    });
}