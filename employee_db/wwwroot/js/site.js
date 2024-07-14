// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(() => {
    var today = new Date().toISOString().split('T')[0];
    $("#birth_date").val(today);
})