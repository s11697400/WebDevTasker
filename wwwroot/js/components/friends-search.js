export default class FriendsSearch extends HTMLElement {
    shadowRoot;
templateId = 'friends-search-tpl';
elementId = 'friends-search';
user;
username;
constructor(user) {
    super(); // always call super() first in the ctor.
    this.shadowRoot = this.attachShadow({mode: 'open'});
    
    this.user = user;
   
this.applyTemplate();
   
}
 applyTemplate() {
    let template = document.getElementById(this.templateId);
    console.log(template);
    let clone = template.content.cloneNode(true);

    this.shadowRoot.appendChild(clone);
    const linkElem = document.createElement("link");
    linkElem.setAttribute("rel", "stylesheet");
    linkElem.setAttribute("href", "/css/friends.css");

    this.shadowRoot.appendChild(linkElem);
  
}
addFriends(){
    console.log(this.user);
    console.log(this.username);
    const dataSend = JSON.stringify({
        "userId1": this.user,
        "userName2": this.username,
        "accepted": false
    });
    const output =  fetch('/api/Friendships/', {
        method: 'POST',
        headers: { "Content-Type": "application/json" },
        body:  dataSend
    }).then(response => this.deleteSuccess(response));
}
deleteSuccess(response){
    console.log(response);
    if(response["ok"]){
       location.reload();
    }
}
connectedCallback() {
    

    console.log('item: connected to DOM');


     
    this.applyEventlisteners();
}

disconnectedCallback() {
    console.log('item: disconnected from DOM');

}
 applyEventlisteners() {
    // console.log('Click event in boodschappen-item.js nog niet ingeschakeld.');
    const self = this;
    
    this.shadowRoot.querySelector('div[data-bind="Add"] button').addEventListener('click', () =>{
        const searchValue = self.shadowRoot.querySelector('input[name="username"]').value;
        this.username = searchValue; 
        if(searchValue == "" || searchValue == " "){
                alert("voer een geldige username in");
        }

        console.log(searchValue);
        try {
            this.addFriends();
        } catch (error) {
            
        }
       
        
    });
     
}



}
customElements.define('friends-search', FriendsSearch);

export {FriendsSearch};
