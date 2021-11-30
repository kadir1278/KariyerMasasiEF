$('#birth_date').bootstrapMaterialDatePicker({
    weekStart: 1,
    format: "YYYY-MM-DD",
    time: false,
    lang: 'tr',
    cancelText: 'İptal',
    okText: 'Tamam',
    clearText: 'Temizle',
    nowText: 'Bugün',
    switchOnClick: true,
    clearButton: true,

});
$('.pick-year').yearpicker();
function addEducation() {
    let row = `  <hr /> <div class="row mt-2">
                    <div class="col-md-2">
                        <p class="mb-1">Başlangıç Yılı</p>
                        <input class="form-control pick-year" type="text"
                            name="" readonly>
                    </div>
                    <div class="col-md-2">
                        <p class="mb-1">Bitiş Yılı</p>
                        <input class="form-control pick-year" type="text"
                            name="" readonly>
                    </div>
                    <div class="col-md-2">
                        <p class="mb-1">Seviye</p>
                        <select class="form-control" name="">
                            <option value="Lise">Lise</option>
                            <option value="Önlisans">Önlisans</option>
                            <option value="Lisans">Lisans</option>
                            <option value="Yüksek Lisans">Yüksek Lisans</option>
                            <option value="Doktora">Doktora</option>
                        </select>
                    </div>
                    <div class="col-md-3">
                        <p class="mb-1">Okul</p>
                        <input class="form-control" type="text"
                            name="">
                    </div>
                    <div class="col-md-3">
                        <p class="mb-1">Bölüm</p>
                        <input class="form-control" type="text"
                            name="">
                    </div>
                </div>`;


    $('#schools').append(row);
    $('.pick-year').yearpicker();
}

function addLanguage() {
    let row = document.createElement('div');
    row.classList.add('row');

    let col1 = document.createElement('div');
    col1.classList.add('col-12');
    let p1 = document.createElement('p');
    p1.classList.add('mb-1');
    p1.innerHTML = "Dil";
    col1.appendChild(p1);
    let input1 = document.createElement('input');
    input1.type = "text";
    input1.name = "";
    input1.classList.add("form-control");
    col1.appendChild(input1);
    row.appendChild(col1);

    let col2 = document.createElement('div');
    col2.classList.add('col-md-4');
    let p2 = document.createElement('p');
    p2.classList.add('mb-1');
    p2.innerHTML = "Okuma";
    col2.appendChild(p2);
    let select1 = document.createElement('select');
    select1.classList.add('form-control');
    select1.name = "";
    let s1o1 = document.createElement('option');
    s1o1.value = "Düşük";
    s1o1.innerHTML = "Düşük";
    select1.appendChild(s1o1);
    let s1o2 = document.createElement('option');
    s1o2.value = "Orta";
    s1o2.innerHTML = "Orta";
    select1.appendChild(s1o2);
    let s1o3 = document.createElement('option');
    s1o3.value = "İyi";
    s1o3.innerHTML = "İyi";
    select1.appendChild(s1o3);
    let s1o4 = document.createElement('option');
    s1o4.value = "Anadil";
    s1o4.innerHTML = "Anadil";
    select1.appendChild(s1o4);
    col2.appendChild(select1)
    row.appendChild(col2);

    let col3 = document.createElement('div');
    col3.classList.add('col-md-4');
    let p3 = document.createElement('p');
    p3.classList.add('mb-1');
    p3.innerHTML = "Yazma";
    col3.appendChild(p3);
    let select2 = document.createElement('select');
    select2.classList.add('form-control');
    select2.name = "";
    let s2o1 = document.createElement('option');
    s2o1.value = "Düşük";
    s2o1.innerHTML = "Düşük";
    select2.appendChild(s2o1);
    let s2o2 = document.createElement('option');
    s2o2.value = "Orta";
    s2o2.innerHTML = "Orta";
    select2.appendChild(s2o2);
    let s2o3 = document.createElement('option');
    s2o3.value = "İyi";
    s2o3.innerHTML = "İyi";
    select2.appendChild(s2o3);
    let s2o4 = document.createElement('option');
    s2o4.value = "Anadil";
    s2o4.innerHTML = "Anadil";
    select2.appendChild(s2o4);
    col3.appendChild(select2)
    row.appendChild(col3);

    let col4 = document.createElement('div');
    col4.classList.add('col-md-4');
    let p4 = document.createElement('p');
    p4.classList.add('mb-1');
    p4.innerHTML = "Konuşma";
    col4.appendChild(p4);
    let select3 = document.createElement('select');
    select3.classList.add('form-control');
    select3.name = "";
    let s3o1 = document.createElement('option');
    s3o1.value = "Düşük";
    s3o1.innerHTML = "Düşük";
    select3.appendChild(s3o1);
    let s3o2 = document.createElement('option');
    s3o2.value = "Orta";
    s3o2.innerHTML = "Orta";
    select3.appendChild(s3o2);
    let s3o3 = document.createElement('option');
    s3o3.value = "İyi";
    s3o3.innerHTML = "İyi";
    select3.appendChild(s3o3);
    let s3o4 = document.createElement('option');
    s3o4.value = "Anadil";
    s3o4.innerHTML = "Anadil";
    select3.appendChild(s3o4);
    col4.appendChild(select3)
    row.appendChild(col4);


    document.getElementById('languages').appendChild(row);
}


function addExperience() {
    let row = `
                     <hr />
                    <div class="row">
                        <div class="col-md-2">
                            <p class="mb-1">Başlangıç Yılı</p>
                            <input class="form-control pick-year" type="text" readonly
                                name="">
                        </div>
                        <div class="col-md-2">
                            <p class="mb-1">Bitiş Yılı</p>
                            <input class="form-control pick-year" type="text" readonly
                                name="">
                        </div>
                        <div class="col-md-4">
                            <p class="mb-1">Şirket</p>
                            <input class="form-control" type="text"
                                name="">
                        </div>
                        <div class="col-md-4">
                            <p class="mb-1">Alan</p>
                            <input class="form-control" type="text"
                                name="">
                        </div>
                        <div class="col-12">
                            <p class="mb-1">Açıklama</p>
                            <input class="form-control" type="text"
                                name="">
                        </div>
                    </div>`;
    $('#experiences').append(row);
    $('.pick-year').yearpicker();

}


function addCourse() {
    let row = ` <hr /><div class="row">
                        <div class="col-md-2">
                            <p class="mb-1">Başlangıç Yılı</p>
                            <input class="form-control pick-year" type="text" readonly
                                name="">
                        </div>
                        <div class="col-md-2">
                            <p class="mb-1">Bitiş Yılı</p>
                            <input class="form-control pick-year" type="text" readonly
                                name="">
                        </div>
                        <div class="col-md-4">
                            <p class="mb-1">Kurum Adı</p>
                            <input class="form-control" type="text" name="">
                        </div>
                        <div class="col-md-4">
                            <p class="mb-1">Seminer/Kurs Adı</p>
                            <input class="form-control" type="text"
                                name="">
                        </div>
                    </div>`;

    $('#courses').append(row);
    $('.pick-year').yearpicker();

}

function addKnowledge() {
    let row = document.createElement('div');
    row.classList.add('row');

    let col1 = document.createElement('div');
    col1.classList.add('col-md-3');
    let p1 = document.createElement('p');
    p1.classList.add('mb-1');
    p1.innerHTML = "Seviye";
    col1.appendChild(p1);
    let input1 = document.createElement('input');
    input1.type = "text";
    input1.name = "";
    input1.classList.add("form-control");
    col1.appendChild(input1);
    row.appendChild(col1);

    let col2 = document.createElement('div');
    col2.classList.add('col-9');
    let p2 = document.createElement('p');
    p2.classList.add('mb-1');
    p2.innerHTML = "Program, İşletim Sistemi, Dil, Donanım vb.";
    col2.appendChild(p2);
    let input2 = document.createElement('input');
    input2.type = "text";
    input2.name = "";
    input2.classList.add("form-control");
    col2.appendChild(input2);
    row.appendChild(col2);

    document.getElementById('knowledges').appendChild(row);
}

function addReference() {

    let row = ` <hr /><div class="row">
                        <div class="col-2">
                            <p class="mb-1">Ad Soyad</p>
                            <input class="form-control" type="text" name="">
                        </div>
                        <div class="col-2">
                            <p class="mb-1">Şirket</p>
                            <input class="form-control" type="text" name="">
                        </div>
                        <div class="col-2">
                            <p class="mb-1">Ünvan</p>
                            <input class="form-control" type="text" name="">
                        </div>
                        <div class="col-md-3">
                            <p class="mb-1">E-Posta</p>
                            <input class="form-control" type="text" name="">
                        </div>
                        <div class="col-md-3">
                            <p class="mb-1">Telefon</p>
                            <input class="form-control" type="text" name="">
                        </div>
                    </div>`;

    $('#references').append(row);
}