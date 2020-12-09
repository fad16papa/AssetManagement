//This will call the modal to register a user
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
        let errorBody = "Error! \nPlease contact administrator.";

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
            let successBody = "New user staff has been created!";

            //function call to display the Error Message
            DisplaySuccessModal(successHeader, successBody);

            //function call
            viewUserStaffs();
        }
    }).fail(function () {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);
    });
}

//This will call the modal to register a user
function UpdateUserStaffModal(paramUserStaffId) {
    //DIV placeholder for the Modal
    var placeholderElement = $('#ModalPlaceholder');

    $.ajax({
        method: 'GET',
        url: "/UserStaffs/Update",
        data: ({
            userStaffId: paramUserStaffId
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

function UpdateUserStaff() {

    $.ajax({
        method: 'PUT',
        url: "/UserStaffs/Update",
        data: $("#formUpdateUserStaff").serialize(),
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
            viewUserStaffs();
        }
    }).fail(function () {

        let errorHeader = "System Error!";
        let errorBody = "Error! \nPlease contact administrator.";

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
        let errorBody = "Error! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);

    });
}
function realoadUserStaffsPage() {
    window.location.reload();
}