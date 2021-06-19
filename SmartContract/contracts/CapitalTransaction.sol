//SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.0;

import "./UserManager.sol";

contract CapitalTransaction {
    enum CapitalType{NONE,SHORT_TERM_ASSETS, LONG_TERM_ASSETS, LIABILITY, EQUITY}
    enum CapitalStatus{NONE,PENDING,CONFIRMED,CANCELED}
    
    struct Capital {
        uint256 Id;
        uint256 Index;
        string Title;
        string Description;
        CapitalType Type;
        CapitalStatus Status;
        uint256 Value;
    }
    
    Capital[] capitals;
    uint256 counter;
    uint256 indexer;
    mapping(uint256 => uint256) id2index;
    mapping(uint256 => Capital) index2capital;
    uint256 totalCapitalValue;
    UserManager userManager = new UserManager();
    
    constructor(){
        counter = 0;
        totalCapitalValue = 0;
    }
    
    modifier onlyActiveAdmin(){
        require(userManager.isActiveAdmin(msg.sender) == true, "Sender is not an active administrator");
        _;
    }
    
    modifier onlyActiveUser(){
        require(userManager.isActiveUser(msg.sender) == true, "Sender is not an active user");
        _;
    }
    
    function getBalance() public view returns(uint256){
        return totalCapitalValue;
    }
    
    function getCount() public view returns(uint256){
        return counter;
    }
    
    function createCapital(uint256 id, string memory title, string memory description, uint8 capitalType, uint256 value) 
    public onlyActiveUser() {
        require(id >= 0, "Id must is a positive number");
        require(value >= 0, "Value must is a positive number");
        require(capitalType >= 0 && capitalType <= 3 , "Capital type is invalid");
        uint256 index = id2index[id];
        require(index2capital[index].Id == 0, "Capital has already existed");
        
        Capital storage capital = index2capital[index];
        capital.Id = id;
        capital.Index = counter;
        capital.Title = title;
        capital.Description = description;
        capital.Type = CapitalType(capitalType);
        capital.Value = value;
        capital.Status = CapitalStatus.PENDING;
        
        id2index[id] = counter;
        
        counter++;
        capitals.push(capital);
    }
    
    function confirm(uint256 id) public {
        uint256 index = id2index[id];
        require(index2capital[index].Id != 0, "Capital not found");
        require(index2capital[index].Status == CapitalStatus.PENDING, "Capital has already approved");
        
        Capital storage capital = index2capital[index];
        capital.Status = CapitalStatus.CONFIRMED;
        
        if (capital.Type == CapitalType.LIABILITY)
        {
            withdraw(capital.Value);
            return;
        }
        deposite(capital.Value);
    }
    
    function cancel(uint256 id) public {
        uint256 index = id2index[id];
        require(index2capital[index].Id != 0, "Capital not found");
        require(index2capital[id].Status == CapitalStatus.PENDING, "Capital has already approved");
        
        Capital storage capital = index2capital[index];
        capital.Status = CapitalStatus.CANCELED;
    }
    
    function deposite(uint256 value) private{
        totalCapitalValue += value;
    }
    
    function withdraw(uint256 value) private{
        if (totalCapitalValue < value){
            revert("Not have enable balance to withdraw");
        }
        totalCapitalValue -= value;
    }
}
