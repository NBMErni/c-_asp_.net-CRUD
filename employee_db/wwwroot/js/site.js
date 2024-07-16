// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(() => {
    var today = new Date().toISOString().split('T')[0];
    $("#birth_date").val(today);

    $("#data-tables").DataTable({

 

        ajax: {
            url: 'https://localhost:7047/employees/',
            dataSrc: ''
        },
        column: [
            {data: "Id"},
            { data: 'first_name' },
            { data: 'last_name' },
            { data: 'gender' },
            { data: 'birth_date' },
            { data: 'email' },
            {data: 'position'}
        ],
        columnDefs: [
            {
                targets: 0,
                render: function (data, type, row) {
                    return row.first_name +  " " + row.last_name;
                }
            },
            {
                targets: 1,
                render: function (data, type, row) {
                    return row.gender;
                }
            },
            {
                targets: 2,
                render: function (data, type, row) {
                    return row.birth_date;
                }
            },
            {
                targets: 3,
                render: function (data, type, row) {
                    return row.position;
                }
            },
            {
                targets: 4,
                render: function (data, type, row) {
                    return `<a class="btn btn-warning text-white" href="/Employee/Edit/${row.id}">Edit</a>
                    <a class="btn btn-danger text-white" href="javascript:void(0);" onclick="handleDeleteEmployee(${row.id})">Delete</a>`
                }
            },
        ]
 
    });
   
})




const  handleDeleteEmployee = (id) => {
    if (confirm("Are you sure you want to delete this employee?")) {

      

        $.ajax({
            url: `delete-employee/${id}`,
            type: 'DELETE',
            headers: {
                'Content-Type': 'application/json'    
            },
            success: function (result) {
                alert('Employee deleted successfully.');
                
                $('#data-tables').DataTable().ajax.reload();
            },
            error: function (xhr, status, error) {
                alert('Failed to delete employee.');
                console.error('Error:', error);
            }
        });
    }
}

