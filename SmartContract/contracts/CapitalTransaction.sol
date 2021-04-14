pragma solidity ^0.8.0;

contract CapitalTransaction {
    enum CapitalType{SHORT_TERM_ASSETS, LONG_TERM_ASSETS, LIABILITY, EQUITY}

    struct Capital {
        uint256 Id;
        uint256 Index;
        string Title;
        string Description;
        CapitalType Type;
        uint256 Value;
        address Creator;
        address Approver;
    }

    address[] public users;
    CapitalTransaction[] public transactions;
    
}
