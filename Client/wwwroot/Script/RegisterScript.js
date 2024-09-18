$(document).ready(function () {
    $('#EmpTable').DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            url: "https://localhost:7121/api/Account/EmpData",
            type: "GET",
            dataType: "json",
            "datasrc": "data",
            //success: function (result) {
            //    console.log(result);
            //}
        },
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { "data": "nik" },
            { "data": "email" },
            { "data": "fullName" },
            { "data": "username" },
            { "data": "deptName" },
            {
                "render": function (data, type, row) {
                    return '<div style="display:flex; justify-content:center; gap:5px;"><button type="button" id="editEmpButton" class="btn btn-warning" data-tooltip="tooltip" data-placement="top" title="Edit Data" onclick="editEmp(\'' + row.nik + '\')"> <i class="fas fa-pencil-alt"></i></button > ' +
                        ' | <button type="button" id="deleteEmpButton" class="btn btn-danger" data-tooltip="tooltip" data-placement="top" title="Delete Data" onclick="deleteEmp(\'' + row.nik + '\')"><i class="fas fa-trash"></i></button></div > ';
                }
            }
        ]
    });
});

$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7121/api/Department",
        type: "GET",
        dataType: "json",
    }).then((result) => {
            $('#department').empty();
            $('#department').append('<option>== Pilih Departemen ==</option>');
            $.each(result.data, function (index, department) {
                $('#department').append('<option value="' + department.dept_ID + '">' + department.dept_Name + '</option>');
            });
    });
});

$('#addEmp').on('click', function () {
    //debugger;
    var emp = new Object();
    emp.firstName = $('#firstName').val();
    emp.lastName = $('#lastName').val();
    emp.email = $('#email').val();
    emp.dept_ID = $('#department').val();
    $.ajax({
        url: "https://localhost:7121/api/Account",
        type: "POST",
        data: JSON.stringify(emp),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            Swal.fire({
                icon: "success",
                title: result.message,
                showConfirmButton: false,
                timer: 1500
            });
            $('#EmpTable').DataTable().ajax.reload();
        } else {
            //alert(result.message);
            Swal.fire(result.message);
        }
    }).catch((err) => {
        //debugger
        Swal.fire({
            icon: "error",
            title: err.responseJSON.message,
            showConfirmButton: false,
            timer: 2000
        });
    });
});

function editEmp(id) {

}

function deleteEmp(id) {

}


// menampilkan tooltip
$('[data-tooltip="tooltip"]').tooltip({ trigger: "hover" });

// menampilkan tooltip (ajax)
$(document).ajaxComplete(function () {
    $('[data-tooltip="tooltip"]').tooltip({
        trigger: "hover",
    });
});

// reset form setiap kali modal dibuka
$('#exampleModal').on('hidden.bs.modal', function () {
    $("#regForm").trigger('reset');
});

// focus ke firstName saat modal dibuka
$('#exampleModal').on('shown.bs.modal', function () {
    $("#firstName").focus();
});

// saat tombol dengan id 'modalButton' diklik, tombol yang muncul pada modal hanya tombol tambah department (id 'addEmp') saja
document.getElementById('modalButton').addEventListener('click', function () {
    document.getElementById('addEmp').style.display = 'block';
    document.getElementById('editEmp').style.display = 'none';
});