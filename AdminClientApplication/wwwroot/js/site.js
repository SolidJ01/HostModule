// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SubmitProfile() {
    description = document.getElementById("description");
    descriptionhidden = document.getElementById("descriptionhidden");

    descriptionhidden.value = new DOMParser().parseFromString(description.innerHTML, 'text/html').body.textContent;

    form = document.getElementById("profileform");
    form.submit();
}
