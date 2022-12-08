var dtable;
var selectedBus = document.getElementById("busSelect")
var selectedSeat = document.getElementById("seatSelect")
$(document).ready(function () {

    selectedBus?.value?changeSeats():null;
    dtable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Customs/Booking/GetAllBooking",
            "type": "GET",
            "datatype": "json",
        },
        "columns": [

            { "data": "bus.busNumber" },
            { "data": "seat.seatNo" },
            { "data": "bookingDate" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Customs/Booking/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a>
                     <a onclick="RemoveProduct('/Customs/Booking/Delete?id=${data}')" > <i class="bi bi-trash"></i></a>`;
                }
            }

        ]
    });

});


function RemoveProduct(url) {
    Swal.fire({
        title: 'Are you sure',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes,delete it!'



    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'Delete',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })

}

function changeSeats() {
    busId = selectedBus.value;
    $("#droplist").empty();
    $.ajax({
        "url": `/Admin/Seat/GetAllSeatByBusId/${busId}`,
        "type": "GET",
        "datatype": "json",
        "success": function (seats) {
            console.log(seats)
            seats.data.forEach((seat) => {
                $('#seatSelect').append($('<option>', {
                    value: seat.id,
                    text: seat.seatNo
                }));
            })
        }
    })

}