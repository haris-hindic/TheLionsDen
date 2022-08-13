// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'room_amenity_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RoomAmenityResponse _$RoomAmenityResponseFromJson(Map<String, dynamic> json) =>
    RoomAmenityResponse()
      ..roomAmenityId = json['roomAmenityId'] as int?
      ..roomId = json['roomId'] as int?
      ..amenityId = json['amenityId'] as int?
      ..amenityName = json['amenityName'] as String?
      ..amenity = json['amenity'] == null
          ? null
          : AmenityResponse.fromJson(json['amenity'] as Map<String, dynamic>);

Map<String, dynamic> _$RoomAmenityResponseToJson(
        RoomAmenityResponse instance) =>
    <String, dynamic>{
      'roomAmenityId': instance.roomAmenityId,
      'roomId': instance.roomId,
      'amenityId': instance.amenityId,
      'amenityName': instance.amenityName,
      'amenity': instance.amenity,
    };
