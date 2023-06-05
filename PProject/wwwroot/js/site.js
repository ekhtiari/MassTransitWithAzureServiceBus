function addNewContact(){
    $.ajax({
        url:'/Contact/Index/',
        success:function(e){
            if(e==="ok"){
                $("#result").text("new contact added");
            }
        }
    })
}

function addNewOrder(){
    $.ajax({
        url:'/Order/Index/',
        success:function(e){
            if(e==="ok"){
                $("#result").text("new order added");
            }
        }
    })
}
