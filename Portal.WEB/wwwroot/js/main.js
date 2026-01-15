function showToast() {
    document.getElementById("submit").onclick = function () {
        var toastElList = [].slice.call(document.querySelectorAll('.toast'))
        var toastList = toastElList.map(function (toastEl) {
            return new bootstrap.Toast(toastEl)
        })
        toastList.forEach(toast => toast.show())
    }
}

//Для экспорта оборудования в Excel
function downloadFileFromBytes(fileName, byteArray) {
    var blob = new Blob([new Uint8Array(byteArray)], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
}