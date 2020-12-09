﻿//This will call the modal to register a user
function CreateUserStaffModal() {
    //DIV placeholder for the Modal
    var placeholderElement = $('#ModalPlaceholder');

    $.ajax({
        method: 'GET',
        url: "/UserStaffs/Create",

    }).done(function (data, statusText, xhdr) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    }).fail(function (xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error displaying the Register User window! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function CreateUserStaff() {

    $.ajax({
        method: 'POST',
        url: "/UserStaffs/Create",
        data: $("#formCreateUserStaff").serialize(),
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
            let successBody = "New asset has been created!";

            //function call to display the Error Message
            DisplaySuccessModal(successHeader, successBody);

            //function call
            viewAssets();
        }
    }).fail(function () {

        let errorHeader = "System Error!";
        let errorBody = "Error adding a user account! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function UpdateUserStaffModal(paramAssetId) {
    //DIV placeholder for the Modal
    var placeholderElement = $('#ModalPlaceholder');

    $.ajax({
        method: 'GET',
        url: "/Assets/Update",
        data: ({
            AssetId: paramAssetId
        })
    }).done(function (data, statusText, xhdr) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    }).fail(function (xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error displaying the Register User window! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function UpdateUserStaff() {

    $.ajax({
        method: 'PUT',
        url: "/Assets/Update",
        data: $("#formUpdateAsset").serialize(),
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
            let successBody = "Success updating asset";

            //function call to display the Error Message
            DisplaySuccessModal(successHeader, successBody);

            //function call
            viewAssets();
        }
    }).fail(function () {

        let errorHeader = "System Error!";
        let errorBody = "Error adding a user account! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

function viewUserStaffs() {
    $.ajax({
        method: 'GET',
        url: "/UserStaffs/Index",
    }).done(function (data, statusText, xhdr) {

    }).fail(function (xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error displaying the user's list! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);

    });
}
function realoadUserStaffsPage() {
    window.location.reload();
}