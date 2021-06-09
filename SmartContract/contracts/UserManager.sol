//SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.0;

contract UserManager{

    struct User{
        uint256 Index;
        address Address;
        bool IsActive;
        bool IsAdmin;
    }
    
    User[] users;
    mapping(address => User) address2user;
    uint256 counter;
    
    modifier onlyActiveAdmin(){
        require(address2user[msg.sender].Address != address(0), "Sender is not a user");
        require(address2user[msg.sender].IsAdmin == true, "Sender is not an administrator");
        require(address2user[msg.sender].IsActive == true, "Sender has been deactive");
        _;
    }
    
    modifier onlyActiveUser(){
        require(address2user[msg.sender].Address != address(0), "Sender is not a user");
        require(address2user[msg.sender].IsActive == true, "Sender has been deactived");
        _;
    }
    
    constructor(){
        counter = 1;
        User storage user = address2user[msg.sender];
        user.Address = msg.sender;
        user.IsActive = true;
        user.IsAdmin = true;
        
        users.push(user);
    }
    
    function createUser(address userAddress) public onlyActiveUser{
        require(address2user[userAddress].Address == address(0), "User with this address has already exitsted");
        
        User storage user = address2user[userAddress];
        user.Address = userAddress;
        user.IsActive = true;
        user.IsAdmin = false;
        user.Index = counter;
        
        users.push(user);
        counter++;
    }
    
    function activeUser(address userAddress) public onlyActiveAdmin{
        require(address2user[userAddress].Address != address(0), "User not found");
        require(userAddress != msg.sender, "User can not active themself");
        
        User storage user = address2user[userAddress];
        user.IsActive = true;
        users[user.Index].IsActive = true;
    }
    
    function deactiveUser(address userAddress) public onlyActiveAdmin{
        require(address2user[userAddress].Address != address(0), "User not found");
        require(userAddress != msg.sender, "User can not deactive themself");
        
        User storage user = address2user[userAddress];
        user.IsActive = false;
        users[user.Index].IsActive = false;
    }
    
    function setAdmin(address userAddress) public onlyActiveAdmin{
        require(address2user[userAddress].Address != address(0), "User not found");
        require(userAddress != msg.sender, "User can not set admin themself");
        
        User storage user = address2user[userAddress];
        user.IsAdmin = true;
        users[user.Index].IsAdmin = true;
    }
    
    function unsetAdmin(address userAddress) public onlyActiveAdmin{
        require(address2user[userAddress].Address != address(0), "User not found");
        require(userAddress != msg.sender, "User can not unset admin themself");
        
        User storage user = address2user[userAddress];
        user.IsAdmin = false;
        users[user.Index].IsAdmin = false;
    }
    
    function getCounter() public view returns(uint256){
        return counter;
    }
    
    function getUsers() public onlyActiveUser view returns(address[] memory, uint256[] memory, bool[] memory, bool[] memory){
        address[] memory addressList = new address[](counter);
        uint256[] memory indexList = new uint256[](counter);
        bool[] memory activeList = new bool[](counter);
        bool[] memory adminList = new bool[](counter);
        for (uint256 i = 0; i < counter; i++){
            addressList[i] = users[i].Address;
            indexList[i] = users[i].Index;
            activeList[i] = users[i].IsActive;
            adminList[i] = users[i].IsAdmin;
        }
        return (addressList,indexList,activeList,adminList);
    }
    
    function getUser(address userAddress) public onlyActiveUser view returns(address, uint256, bool, bool){
        require(address2user[userAddress].Address != address(0), "User not found");
        
        return(userAddress, address2user[userAddress].Index, address2user[userAddress].IsActive, address2user[userAddress].IsAdmin);
    }
    
    function isActiveAdmin(address userAddress) public view returns(bool){
        require(address2user[msg.sender].Address != address(0), "User not found");
        
        return address2user[userAddress].IsAdmin && address2user[userAddress].IsActive;
    }
    
    function isActiveUser(address userAddress) public view returns(bool){
        require(address2user[msg.sender].Address != address(0), "User not found");
        
        return address2user[userAddress].IsActive;
    }
}