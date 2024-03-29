function CreateLicenseModal() {
    //DIV placeholder for the Modal
    var placeholderElement = $('#ModalPlaceholder');

    $.ajax({
        method: 'GET',
        url: "/License/Create",

    }).done(function (data, statusText, xhdr) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    }).fail(function (xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function CreateLicense() {

    var paramRadioButton = $("input[name='radioButton']:checked").val(); 
    var formData = $("#formCreateLicense").serializeArray(); 

    formData.push({ name: "Expiration", value: paramRadioButton });

    if (paramRadioButton === 'No') {
        formData.push({ name: "ExpiredOn", value: document.getElementById('expiredOnValue') });
    }

    $.ajax({
        method: 'POST',
        url: "/License/Create",
        data: $.param(formData),
    }).done(function (data) {

        var newBody = $('.modal-body', data);
        var placeholderElement = $('#ModalPlaceholder');
        placeholderElement.find('.modal-body').replaceWith(newBody);

        // find IsValid input field and check it's value
        // if it's valid then hide modal window
        var isValid = newBody.find('[name="IsValid"]').val() == 'True';
        if (isValid) {
            placeholderElement.find('.modal').modal('hide');

            let successHeader = "User Action!";
            let successBody = "New user staff has been created!";

            //function call to display the Error Message
            DisplaySuccessModal(successHeader, successBody);

            //function call
            viewAssets();
        }
    }).fail(function () {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function UpdateLicenseModal(paramLicenseId)
{
    //DIV placeholder for the Modal
    var placeholderElement = $('#ModalPlaceholder');

    $.ajax({
    method: 'GET',
        url: "/License/Update",
        data: ({
            licenseId: paramLicenseId
        })
    }).done(function(data, statusText, xhdr) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    }).fail(function(xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function UpdateLicense() {
    var paramRadioButton = $("input[name='radioButton']:checked").val();
    var formData = $("#formUpdateLicense").serializeArray();

    formData.push({ name: "Expiration", value: paramRadioButton });

    $.ajax({
        method: 'PUT',
        url: "/License/Update",
        data: $.param(formData),
    }).done(function (data) {

        var newBody = $('.modal-body', data);
        var placeholderElement = $('#ModalPlaceholder');
        placeholderElement.find('.modal-body').replaceWith(newBody);

        // find IsValid input field and check it's value
        // if it's valid then hide modal window
        var isValid = newBody.find('[name="IsValid"]').val() == 'True';
        if (isValid) {
            placeholderElement.find('.modal').modal('hide');

            let successHeader = "User Action!";
            let successBody = "New user staff has been created!";

            //function call to display the Error Message
            DisplaySuccessModal(successHeader, successBody);

            //function call
            viewAssets();
        }
    }).fail(function () {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function viewLicense() {
    $.ajax({
        method: 'GET',
        url: "/License/Index",
    }).done(function (data, statusText, xhdr) {

    }).fail(function (xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);

    });
}

function viewLicenseDetails(paramLicenseId) {
    //DIV placeholder for the Modal
    var placeholderElement = $('#ModalPlaceholder');

    $.ajax({
        method: 'GET',
        url: "/License/ViewLicense",
        data: ({
            LicenseId: paramLicenseId
        })

    }).done(function (data, statusText, xhdr) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    }).fail(function (xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function viewAssignLicenseUser(paramLicenseId) {
    //DIV placeholder for the Modal
    var placeholderElement = $('#ModalPlaceholder');

    $.ajax({
        method: 'GET',
        url: "/License/AssignLicenseUser",
        data: ({
            licenseId: paramLicenseId
        })

    }).done(function (data, statusText, xhdr) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    }).fail(function (xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function AssignLicenseUser() {

    $.ajax({
        method: 'POST',
        url: "/License/AssignLicenseUser",
        data: $("#formAssignLicenseUser").serialize(),
    }).done(function (data) {

        var newBody = $('.modal-body', data);
        var placeholderElement = $('#ModalPlaceholder');
        placeholderElement.find('.modal-body').replaceWith(newBody);

        // find IsValid input field and check it's value
        // if it's valid then hide modal window
        var isValid = newBody.find('[name="IsValid"]').val() == 'True';
        if (isValid) {
            placeholderElement.find('.modal').modal('hide');

            let successHeader = "User Action!";
            let successBody = "License successfully assigned to user!";

            //function call to display the Error Message
            DisplaySuccessModal(successHeader, successBody);

            //function call
            viewLicense();
        }
    }).fail(function () {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function viewAssignLicenseAsset(paramLicenseId) {
    //DIV placeholder for the Modal
    var placeholderElement = $('#ModalPlaceholder');

    $.ajax({
        method: 'GET',
        url: "/License/AssignLicenseAsset",
        data: ({
            licenseId: paramLicenseId
        })

    }).done(function (data, statusText, xhdr) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    }).fail(function (xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function AssignLicensAsset() {

    $.ajax({
        method: 'POST',
        url: "/License/AssignLicenseAsset",
        data: $("#formAssignLicenseAsset").serialize(),
    }).done(function (data) {

        var newBody = $('.modal-body', data);
        var placeholderElement = $('#ModalPlaceholder');
        placeholderElement.find('.modal-body').replaceWith(newBody);

        // find IsValid input field and check it's value
        // if it's valid then hide modal window
        var isValid = newBody.find('[name="IsValid"]').val() == 'True';
        if (isValid) {
            placeholderElement.find('.modal').modal('hide');

            let successHeader = "User Action!";
            let successBody = "License successfully assigned to asset!";

            //function call to display the Error Message
            DisplaySuccessModal(successHeader, successBody);

            //function call
            viewLicense();
        }
    }).fail(function () {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function realoadLicensePage() {
    window.location.reload();
}

function disable() {
    document.getElementById("expiration").style.visibility = "hidden";
}
function enable() {
    document.getElementById("expiration").style.visibility = "visible";
}