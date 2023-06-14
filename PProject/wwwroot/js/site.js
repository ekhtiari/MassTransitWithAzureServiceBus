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
function ScheduledContact(){
    $.ajax({
        url:'/Contact/SendScheduledMessage/',
        success:function(e){
            if(e==="ok"){
                $("#result").text("new contact added after 10 sec");
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

function getResult(){
    $.ajax({
        url:'/Contact/GetContactState/',
        data:{'family': $("#txtFamily").val()},
        success:function(e){
            $("#result").text(e);
        }
    })
}
