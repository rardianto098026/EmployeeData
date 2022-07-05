function ValidateNumber(e) {    
    var evt = (e) ? e : window.event;
    var charCode = (evt.keyCode) ? evt.keyCode : evt.which;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {        
        //if (charCode == 190 || charCode == 86) {

        //}
        //else {
            return false;
        //}
    }
    return true;
};

function numberWithCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

function ComaDecimal(value) {
    if (event.which >= 37 && event.which <= 40) return;
    $(value).val(function (index, value) {
        value = value.replace(/,/g, '');
        return numberWithCommas(value);
    });
}

function padZeroes(number, length) {
    var my_string = '' + number;
    while (my_string.length < length) {
        my_string = '0' + my_string;
    }

    return my_string;
}

function formatDate(date) {
    var newdate = date.split("/")
    var yy = newdate[2]
    var mm = newdate[1]
    var dd = newdate[0]
    return yy + '/' + mm + '/' + dd;
}

//function formatRupiah(bilangan, prefix) {

//    var number_string = bilangan.replace(/[^,\d]/g, '').toString(),
//        split = number_string.split(','),
//        sisa = split[0].length % 3,
//        rupiah = split[0].substr(0, sisa),
//        ribuan = split[0].substr(sisa).match(/\d{1,3}/gi);

//    if (ribuan) {
//        separator = sisa ? '.' : '';
//        rupiah += separator + ribuan;
//    }

//    rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
//    return prefix == undefined ? rupiah : (rupiah ? rupiah : '');
//    //return prefix == undefined ? rupiah : (rupiah ? 'Rp. ' + rupiah : '');
//}