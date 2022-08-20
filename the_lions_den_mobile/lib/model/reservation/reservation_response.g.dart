// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'reservation_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ReservationResponse _$ReservationResponseFromJson(Map<String, dynamic> json) =>
    ReservationResponse()
      ..reservationId = json['reservationId'] as int?
      ..totalPrice = (json['totalPrice'] as num?)?.toDouble()
      ..arrival = json['arrival'] == null
          ? null
          : DateTime.parse(json['arrival'] as String)
      ..departure = json['departure'] == null
          ? null
          : DateTime.parse(json['departure'] as String)
      ..specialRequests = json['specialRequests'] as String?
      ..estimatedArrivalTime = json['estimatedArrivalTime'] as String?
      ..status = json['status'] as String?
      ..userId = json['userId'] as int?
      ..roomId = json['roomId'] as int?
      ..roomName = json['roomName'] as String?
      ..userFullName = json['userFullName'] as String?
      ..facilityNames = json['facilityNames'] as String?;

Map<String, dynamic> _$ReservationResponseToJson(
        ReservationResponse instance) =>
    <String, dynamic>{
      'reservationId': instance.reservationId,
      'totalPrice': instance.totalPrice,
      'arrival': instance.arrival?.toIso8601String(),
      'departure': instance.departure?.toIso8601String(),
      'specialRequests': instance.specialRequests,
      'estimatedArrivalTime': instance.estimatedArrivalTime,
      'status': instance.status,
      'userId': instance.userId,
      'roomId': instance.roomId,
      'roomName': instance.roomName,
      'userFullName': instance.userFullName,
      'facilityNames': instance.facilityNames,
    };
