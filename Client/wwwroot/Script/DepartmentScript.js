$(document).ready(function () {
    $('#DepartmentTable').DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            url: "https://localhost:7121/api/Department",
            type: "GET",
            datatype: "json",
            success: function (result) {
                console.log(result);
            },
        }
    });
});
