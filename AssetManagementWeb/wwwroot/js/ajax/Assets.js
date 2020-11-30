//This will call the modal to register a user
function CreateAsset()
{
    //DIV placeholder for the Modal
    var placeholderElement = $('#staticBackdrop');

    //ajax call to the controller
    //url.action(Action,Controller)
    //data: Paramater passed
    $.ajax({
    method: 'GET',
        url: "/Assets/Create",

    }).done(function(data, statusText, xhdr) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
    }).fail(function(xhdr, statusText, errorText) {

        let errorHeader = "System Error!";
        let errorBody = "Error displaying the Register User window! \nPlease contact administrator.";

        //function calln to display the Error Message
        DisplayErrorModal(errorHeader, errorBody);

    });
}