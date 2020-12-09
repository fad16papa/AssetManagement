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
    var formData = $("#formCreateLicense").serialize(); 

    $.ajax({
        method: 'POST',
        url: "/License/Create",
        //data: { licenseDTO: formData, radioBtn: paramRadioButton},
        data: $("#formCreateLicense").serialize(),
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
function realoadLicensePage() {
    window.location.reload();
}

function disable() {
    document.getElementById("expiration").disabled = true;
}
function enable() {
    document.getElementById("expiration").disabled = false;
}