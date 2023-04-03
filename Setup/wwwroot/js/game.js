import {Heart} from "./components/heart.js";

$(document).ready(function () {
var HeartElement = customElements.define('heart-element', Heart);
   
    const person = $(".game-person");
    const comet = $(".game-object");
    
    const maxY = parseInt($(".game").css("height")) - 50;
    const minY = 0;

    const personX = parseInt(person.css("left"));
    let timeMovingObject = 2000;
    let amountOfLives = 3;
    let score = 0;
    score = setInterval(() => {
        
            score += 1;
            $(".score").text("Score: "+score);
    }, 10);

    function getHeight () {
        return parseInt(person.css("top"));
    }

    let beingHit = false;
    let cometPositon = setInterval(() => {

        if(parseInt(comet.css("left")) < 49 + personX){
            if(getHeight() - 100 < parseInt(comet.css("top")) && getHeight() > parseInt(comet.css("top"))){
                beingHit = true;
            }
            
        }else{
            if(beingHit){
                onHit();
                beingHit = false;
            }
        }
  
    }, 1)
    //randomize comet height
    setInterval(() =>{
        const randomHeight = Math.round(Math.floor(Math.random() * maxY - 50) / 10) * 10;
        comet.css("top", randomHeight);
     
    }, timeMovingObject);
    
  
    document.addEventListener("keydown", function(keydownEvent){
        switch (keydownEvent.key) {
            case "ArrowUp":
                move('up');
                break;
            case "ArrowDown":
                move('down');
                break;
            default:
                break;
        }
    });
    document.getElementById("upBtn").addEventListener('click', function () {
        move('up');
    });
    document.getElementById("downBtn").addEventListener('click', function () {
        move('down');
    });
    function onHit(){
        amountOfLives -= 1;
        let heartContainer = document.querySelector("heart-element").shadowRoot.childNodes;
        heartContainer[heartContainer.length - 1].remove();
        if (amountOfLives <= 0) {
            sendGameData();
        }
        
      
    } 
    function sendGameData () {
        var dataSend = new FormData();
        dataSend.append("PlayerId", document.getElementById("playerId").value);
        dataSend.append("score", score);

        const output =  fetch('/api/Game/', {
            method: 'PUT',
            body: dataSend
        }).then(response => putSuccess(response));
    }
    function putSuccess(response){
    
        if (response["ok"]) {
            alert("GAME OVER!! Je score is: " + score);
            //Herlaad pagina
            location.reload();
        }
    }
    function move(direction) {    
            if(direction == "up"){
                if(minY < getHeight()){
                    person.css("top", getHeight() - 10);
                }
            }
            else if(direction == "down"){
                if(maxY > getHeight()){
                    person.css("top", getHeight() + 10);
                }
            }
        }
});
window.addEventListener("keydown", function(e) {
    if(["Space","ArrowUp","ArrowDown","ArrowLeft","ArrowRight"].indexOf(e.code) > -1) {
        e.preventDefault();
    }
}, false);