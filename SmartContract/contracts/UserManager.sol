//SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.0;

contract UserManager{

    struct User{
        uint256 Id; // can have duplicates
        uint256 Index; // is unique
        address Creator;
        bool isActive;
        bool isAdmin;
    }

    mapping(address => User) userMap;
    address[] userAddressList;
    uint256 userCount; // number of users in list
    uint256 currentIndex;

    constructor(){
        userCount++;
        currentIndex++;

        address userAddress = address(0x7e576E3FFdFf96581f035B29B2E084299b72900c);
        userMap[userAddress] = User(userCount, currentIndex, msg.sender, true, true);
        userAddressList.push(userAddress);
    }

    modifier onlyAdmin(){
        require(userMap[msg.sender].Id != 0, "User is unknown.");
        require(userMap[msg.sender].isAdmin == true, "Admin only.");
        _;
    }

    function getAllUsers() public view returns(address[] memory){
        return userAddressList;
    }

    function addUser(address userAddress) public{
        require(userMap[userAddress].Id == 0, "User already has.");

        userCount++;
        currentIndex++;

        userMap[userAddress] = User(userCount, currentIndex, msg.sender, false, false);
        userAddressList.push(userAddress);
    }

    function setActive(address userAddress ,bool isActive) public onlyAdmin(){
        require(userMap[userAddress].Id != 0, "User not found.");
        userMap[userAddress].isActive = isActive;
    }

    function setAdmin(address userAddress, bool isAdmin) public onlyAdmin(){
        require(userMap[userAddress].Id != 0, "User not found.");
        userMap[userAddress].isAdmin = isAdmin;
    }
}